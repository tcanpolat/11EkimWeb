namespace _08_CustomHelpers.Helpers
{
    public static class StringHelper
    {
        // Gelen string ifadenin sadece ilk harfini büyük yapıp geri kalanını küçük yaparak döndüren metot
        public static string CapitalizeForFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return char.ToUpper(input[0]) + input.Substring(1).ToLower();
        }

        // Cümledeki her kelimenin ilk harfini büyük yapıp geri kalanını küçük yaparak döndüren metot
        public static string CapitalizeWord(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            var words = input.Split(' '); // hava çok güzel -> [hava, çok, güzel]

            for (int i = 0; i < words.Length; i++)
            {
                words[i] = CapitalizeForFirstLetter(words[i]);
            }

            return string.Join(" ", words);
        }
    }
}
