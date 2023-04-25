namespace Text
{
  public static class Text
  {
    /// <summary>
    /// Returns the input string with the first character converted to uppercase.
    /// </summary>
    public static string FirstLetterToUpperCase(string str)
    {
      if (string.IsNullOrEmpty(str))
        throw new ArgumentException("String is empty.");

      char[] a = str.ToCharArray();
      a[0] = char.ToUpper(a[0]);
      return new string(a);
    }
  }
}
