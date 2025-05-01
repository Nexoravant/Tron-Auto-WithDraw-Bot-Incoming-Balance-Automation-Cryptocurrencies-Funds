namespace Withdraw.Cash.Contracts.Constants;
public static class RabbitMQQueues
{
    public const string CreateCashWithdrawRequestQueue = "proxy__createCashWithdrawRequestQueue";
    public const string GetStatusByClientIdQueue = "proxy__getStatusByClientIdQueue";
    public const string GetStatusByWithdrawRequestIdQueue = "proxy__getStatusByWithdrawRequestIdQueue";
}
