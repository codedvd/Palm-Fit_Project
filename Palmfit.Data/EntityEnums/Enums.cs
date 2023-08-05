using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Palmfit.Data.EntityEnums
{
    public enum Gender
    {
        Male,
        Female
    }

    public enum HeightUnit
    {
        cm,
        inches,
        ft,
    }

    public enum WeightUnit
    {
        Kg,
        Lbs
    }

    public enum GenoType
    {
        AA,
        AS,
        SS,
        SC
    }

    public enum BloodGroup
    {
        A,
        B,
        AB,
        O
    }

    public enum SubscriptionType
    {
        Basic,
        Standard,
        Premium
    }

    public enum TransactionType
    {
        Deposit,
        Withdrawal,
        Transfer,
        Purchase,
        Payment,
        Refund,
        Fee,
        Expense,
        Donation,
        Reward,
        Subscription,
        Bonus
    }

    public enum TransactionChannel
    {
        OnlineOnlineBanking,
        ATM,
        MobileApp,
        POS,
        BankBranch,
        WireTransfer,
        Cryptocurrency,
        Cash,
        Check,
        BillPayment,
        DirectDebit,
        GiftCard,
        Venmo,
        PayPal,
        ApplePay,
        GooglePay,
        SamsungPay
    }

    public enum WalletType
    {
        Personal,
        Business,
        Savings,
        Travel,
        Crypto,
        GiftCard,
        Online,
        Joint,
        Child,
        Charity
    }
}