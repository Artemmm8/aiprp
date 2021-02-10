namespace bondarchuk_zhukovskyLab2_server_
{
    class Account
    {
        public decimal Sum { get; set; }
        public int MyProperty { get; set; }

        public Account(decimal sum = 0.00m)
        {
            Sum = sum;
        }
    }
}
