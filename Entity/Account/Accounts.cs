using AutoCount.Authentication;
using AutoCount.Data;
using AutoCount.GL.AccountMaintenance;
using AutoCount.RegistryID;
using PlugIn_1.Entity.General_Maintainance;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PlugIn_1.Entity.Accounts
{
    internal class Accounts
    {
        private readonly UserSession session;

        private readonly AccountCommand cmd;

        /*
         [------------------Account Type-----------------]

        Description		        UBS	Auto Count

        Capital			        A	CP
        Retained Earning	    A	RE
        Fixed Assets		    D	FA
        Other Assets		    E	OA
        Current Assets		    F	CA
        Current Liabilities	    G	CL
        Long Term Liabilities	B	LL
        Other Liabilities	    C	OL
        Sales			        H	SL
        Sales Adjustments	    I	SA
        Cost of Good Sold	    J	CO
        Other Incomes		    L	OI
        Extra-Ordinary Income	S	EI
        Expenses		        M	EP
        Taxation		        P	TX
        Appropriation A/C	    T	AP
        */

        private readonly Dictionary<string, string> acc_type_map = new Dictionary<string, string>()
        {
            {"A", "CP/RE"},  // Capital/Retained Earning
            {"D", "FA"},     // Fixed Assets
            {"E", "OA"},     // Other Assets
            {"F", "CA"},     // Current Assets
            {"G", "CL"},     // Current Liabilities
            {"B", "LL"},     // Long Term Liabilities
            {"C", "OL"},     // Other Liabilities
            {"H", "SL"},     // Sales
            {"I", "SA"},     // Sales Adjustments
            {"J", "CO"},     // Cost of Good Sold
            {"L", "OI"},     // Other Incomes
            {"S", "EI"},     // Extra-Ordinary Income
            {"M", "EP"},     // Expenses
            {"P", "TX"},     // Taxation
            {"T", "AP"},     // Appropriation A/C
        };

        /*
        [-------------Special Account Type-------------]

        Description		        UBS	Auto Count

        Accumulated Dprc. Acc.	AD	SAD
        Bank Account		    BK	SBK
        Balance Sheet Stock	    BS	SBS
        Cash Account		    CA	SCH
        Credit Card Rcv. Acc.	CC	SBK
        Credit Account		    CR	-
        Creditor Control	    -	SCC
        Closing Stock		    CS	SCS
        Display 0 Account	    D0	-
        Debtor Control		    -	SDC
        Deposit Account		    - 	SDP
        Depreciation Account	DP	-
        Fixed Assets		    -	SFA
        Header			        HD	-
        Manufacturing Account	MA	-
        Other Expenses		    OE	-
        Other Receipt		    OR	-
        Opening Stock		    OS	SOS
        Profit Account		    PA	SRE
        Purchase Tax Account	PT	-
        Payment Voucher		    PV	-
        Sales Tax Account	    ST  -
        Subtotal lvl 0		    T0  -
        Subtotal lvl 1		    T1  -
        Subtotal lvl 2		    T2  -
        Total Desc.		        TD  -
        No Subtotal		        TX  -
         */

        private readonly Dictionary<string, SpecialAccType> spAcc_type_map = new Dictionary<string, SpecialAccType>()
        {
            {"AD", SpecialAccType.AccumulatedDepreciation}, // Accumulated Depreciation Account
            {"BK", SpecialAccType.Bank},                    // Bank Account
            {"BS", SpecialAccType.BalanceStock},            // Balance Sheet Stock
            {"CA", SpecialAccType.Cash},                    // Cash Account
            {"CC", SpecialAccType.Bank},                    // Credit Card Received Account
            {"CS", SpecialAccType.CloseStock},              // Closing Stock
            {"D0", SpecialAccType.Normal},                  // Display 0 Account
            {"DP", SpecialAccType.Normal},                  // Depreciation Account
            {"HD", SpecialAccType.Normal},                  // Header
            {"MA", SpecialAccType.Normal},                  // Manufacturing Account
            {"OE", SpecialAccType.Normal},                  // Other Expenses
            {"OR", SpecialAccType.Normal},                  // Other Receipt
            {"OS", SpecialAccType.OpenStock},               // Opening Stock
            {"PA", SpecialAccType.RetainedEarning},         // Profit Account
            {"PT", SpecialAccType.Normal},                  // Purchase Tax Account
            {"PV", SpecialAccType.Normal},                  // Payment Voucher
            {"ST", SpecialAccType.Normal},                  // Sales Tax Account
            {"T0", SpecialAccType.Normal},                  // Subtotal lvl 0
            {"T1", SpecialAccType.Normal},                  // Subtotal lvl 1
            {"T2", SpecialAccType.Normal},                  // Subtotal lvl 2
            {"TD", SpecialAccType.Normal},                  // Total Desc.
            {"TX", SpecialAccType.Normal},                  // No Subtotal

            {"BKPV", SpecialAccType.Bank},
            {"PVCA", SpecialAccType.Cash},
            {"BKPVCA", SpecialAccType.Bank}
        };

        public Accounts(UserSession userSession) 
        {
            session = userSession;

            cmd = AccountCommand.Create(session, session.DBSetting);
        }

        public void CreateNormalAccount(bool isOverwrite, string acc_no, AccountModel accountModel)
        {
            Account account = isOverwrite && hasAccount(acc_no) ? 
                cmd.GetAccount(acc_no) : cmd.NewAccount();

            account.AccNo            = acc_no;
            account.AccType          = setAccType(accountModel.acc_type, accountModel.spAcc_type);
            account.ParentAccNo      = setParentAccNo(acc_no, account.AccType);
            account.Description      = accountModel.desc;
            account.Desc2            = accountModel.desc2;

            account.SpecialAccType   = 
                setSpecialAccType(
                    account, 
                    accountModel.spAcc_type
                );

            account.CurrencyCode     = setCurrency(session.DBSetting, accountModel.currency_code);
            account.CashFlowCategory = 0;

            cmd.SaveAccount(session.DBSetting, account);
        }

        private string setParentAccNo(string acc_no, string acc_type)
        {
            string pan1, pan2 = "";

            char spliter = acc_no.Contains("/") ? '/' : '-';

            string[] parts = acc_no.Split(spliter);
            string front = parts[0];
            string back = parts[1];

            if (!Regex.Match(acc_no, @"(000/|00-0)000$").Success)
            {
                if (!Regex.Match(front, @"^(8|9).*").Success)
                {
                    pan1 = spliter == '/' ?
                    $"{front.Substring(0, 3)}0/000" : // {???}0/000 or
                    $"{front.Substring(0, 3)}-0000";  // {???}-0000

                    //----------------------------------------------------//

                    if (spliter == '/')
                    {
                        pan2 = $"{front.Substring(0, 4)}/000"; // {????}/000
                    }
                    else
                    {
                        string alpha = new string(back.Where(a => char.IsLetter(a)).ToArray());
                        string digit = new string(back.Where(d => char.IsDigit(d)).ToArray());

                        if (alpha.Length == 0)
                        {
                            // {???}-{?}000
                            pan2 = $"{front.Substring(0, 3)}-{back.First()}000";
                        }
                        else
                        {
                            // Instead of {???}-{A}?00 , change to {???}-{?}000
                            pan2 = $"{front.Substring(0, 3)}-{digit.First()}000";
                        }
                    }
                }
                else
                {
                    if (spliter == '/')
                    {
                        pan1 = $"{front.Substring(0, 4)}/000"; // {????}/000
                    }
                    else
                    {
                        string alpha = new string(back.Where(a => char.IsLetter(a)).ToArray());
                        string digit = new string(back.Where(d => char.IsDigit(d)).ToArray());

                        if (alpha.Length == 0)
                        {
                            // {???}-{?}000
                            pan1 = $"{front.Substring(0, 3)}-{back.First()}000";
                        }
                        else
                        {
                            // Instead of {???}-{A}?00 , change to {???}-{?}000
                            pan1 = $"{front.Substring(0, 3)}-{digit.First()}000";
                        }
                    }
                }

                if (hasAccount(pan2, acc_type))
                    return pan2;
                else if (hasAccount(pan1, acc_type))
                    return pan1;
            }

            return null;
        }

        private string setAccType(string acc_type, string spAcc_type)
        {
            if (acc_type != "A") return acc_type_map[acc_type];                  // If is other account type

            string[] cpt_or_rten = acc_type_map[acc_type].Split('/');            // ["CP", "RE"]

            return spAcc_type.ToUpper() == "" ? cpt_or_rten[0] : cpt_or_rten[1]; // Capital or Rtn. Earning
        }

        private SpecialAccType setSpecialAccType(Account account, string spAcc_type)
        {
            string acc_no = account.AccNo;
            string acc_type = account.AccType;
            string desc = account.Description.ToString();
            
            // If is items under "Fixed Assets" (Based on acc. no.)
            if (Regex.Match(acc_no, @"^(2|3)0[1-9](0/|-0)000$").Success && desc.ToUpper().Contains("DEPOSIT"))
            {
                return SpecialAccType.Deposit;
            }
            // If is a Debtor or Creditor control account (Based on acc. no.)
            else if (Regex.Match(acc_no, @"^(3|4)00(0/|-0)000$").Success)
            {
                // Identify the debtor, else is creditor
                bool isDebtorCtrl = Regex.Match(acc_no, @"^300(0/|-0)000$").Success;

                return isDebtorCtrl ? SpecialAccType.DebtorControl : SpecialAccType.CreditorControl;
            }
            // If is a deposit account (No fix account number)
            else if (Regex.Match(acc_no, @"^2[0-9]{2}([0-9]/|-[0-9])[0-9]{3}$").Success)
            {
                bool isFixedAssets = !Regex.Match(acc_no, @"^200(0/|-0)000$").Success &&
                    spAcc_type != "AD" && acc_type == acc_type_map["D"];

                return isFixedAssets ?                                              // If is fixed asset accounts
                    SpecialAccType.FixedAsset : spAcc_type == "AD" ?                // If is acuml. dprec. accounts
                    SpecialAccType.AccumulatedDepreciation : SpecialAccType.Normal; // For "Fixed Assets" account
            }
            // Else other than above cases
            else
            {
                return spAcc_type != "" ? spAcc_type_map[spAcc_type] : SpecialAccType.Normal;
            }
        }

        private string setCurrency(DBSetting dbSetting, string currency_code)
        {
            Currencies currencies = new Currencies(session);

            return currencies.hasCurrency(currency_code) ?
                currency_code : currency_code == "" ?
                    DBRegistry.Create(dbSetting).GetString(new LocalCurrencyCode()) :
                    currencies.CreateOrUpdate_Currency(false, currency_code);
        }

        internal bool hasAccount(string acc_no)
        {
            return cmd.GetAccount(acc_no) != null;
        }

        internal bool hasAccount(string acc_no, string acc_type)
        {
            Account account = cmd.GetAccount(acc_no);

            return account != null && account.AccType == acc_type;
        }

        internal bool isDebtorAcc(string acc_no)
        {
            return Regex.Match(acc_no, @"^$").Success;
        }

        internal bool isCreditorAcc(string acc_no)
        {
            return Regex.Match(acc_no, @"^$").Success;
        }
    }

    internal class AccountModel
    {
        internal string acc_no { get; set; }
        internal string desc { get; set; }
        internal string desc2 { get; set; }
        internal string acc_type { get; set; }
        internal string spAcc_type { get; set; }
        internal string currency_code { get; set; }
    }
}
