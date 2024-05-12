using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class InputValidator
{
    private static readonly char[] SpaceSeparator = {' '};

    // if a bad word has this length, use contains check instead of exact match
    private static readonly int MinLengthForContainsCheck = 5;

    private static HashSet<string> _badWords;

    private static HashSet<string> BadWords
    {
        get
        {
            if (_badWords != null) return _badWords;

            try
            {
                _badWords = new HashSet<string>();
                var lines = Resources.Load<TextAsset>("badWords").text.Split('\n');
                foreach (var line in lines)
                {
                    var word = line.Trim();
                    if (string.IsNullOrEmpty(word))
                        continue;
                    if (word.StartsWith("#"))
                        continue;

                    _badWords.Add(word);
                }

                return _badWords;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                return _badWords;
            }
        }
    }

    public static string ReplaceDisallowedCharacters(string input, string pattern = @"[^\u007F-\u024FA-Za-z0-9_ ]+")
    {
        var sanitizedInput = Regex.Replace(input, pattern, "");
        return ReplaceMultipleSpace(sanitizedInput);
    }

    public static string ReplaceMultipleSpace(string input)
    {
        return Regex.Replace(input, @"  +", " ");
    }

    public static bool IsInputValid(string input, int minLength = 0, int maxLength = 255)
    {
        if (string.IsNullOrWhiteSpace(input)) return false;
        if (input.Length < minLength) return false;
        if (input.Length > maxLength) return false;

        return IsInputValidAllowPattern(input, @"^[\u007F-\u024FA-Za-z0-9_]+$");
    }

    public static bool IsInputValidAllowEmpty(string input, int minLength, int maxLength)
    {
        if (string.IsNullOrEmpty(input)) return true;
        if (input.Length < minLength) return false;
        if (input.Length > maxLength) return false;

        return IsInputValidAllowPattern(input, @"^[\u007F-\u024FA-Za-z0-9_]+$");
    }

    public static bool IsInputValidAllowWhiteSpace(string input, int minLength, int maxLength)
    {
        if (string.IsNullOrEmpty(input)) return false;
        if (input.Length < minLength) return false;
        if (input.Length > maxLength) return false;

        return IsInputValidAllowPattern(input, @"^[\u007F-\u024FA-Za-z0-9_ ]+$");
    }

    public static bool IsInputValidAllowEmptyOrWhiteSpace(string input, int minLength = 0, int maxLength = 255)
    {
        if (string.IsNullOrEmpty(input)) return true;
        if (input.Length < minLength) return false;
        if (input.Length > maxLength) return false;

        return IsInputValidAllowPattern(input, @"^[\u007F-\u024FA-Za-z0-9_ ]+$");
    }

    private static bool IsInputValidAllowPattern(string input, string pattern)
    {
        if (!Regex.IsMatch(input, pattern)) return false;
        if (pattern.Contains(' '))
        {
            var words = input.Split(SpaceSeparator);
            foreach (var word in words)
                if (IsBadWord(word))
                    return false;
        }
        else if (IsBadWord(input))
        {
            return false;
        }

        return true;
    }

    private static bool IsBadWord(string input)
    {
        var badWords = BadWords;

        // disallow exact matches
        if (badWords.Contains(input)) return true;

        var inputLower = input.ToLower();
        if (badWords.Contains(inputLower)) return true;

        var inputLowerInvariant = input.ToLowerInvariant();
        if (badWords.Contains(inputLowerInvariant)) return true;

        if (input.Length < MinLengthForContainsCheck)
            return false;

        // disallow any/all matches; "bass" -> fail (contains "ass")
        foreach (var badWord in badWords)
        {
            if (badWord.Length < MinLengthForContainsCheck)
                continue;

            if (input.Contains(badWord)) return true;
            if (inputLower.Contains(badWord)) return true;
            if (inputLowerInvariant.Contains(badWord)) return true;
        }

        return false;
    }
}
