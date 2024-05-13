namespace FormFun
{
    static class HelperMethods
    {
        public static bool IsStringInvalid(string _inputString)
        {
            if (string.IsNullOrWhiteSpace(_inputString)) return true;
            return false;
        }
    }
}
