/* Author : 
 * Philippe Matray
 * 
 * Date : 
 * 2014-09-23
 */

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using SimpleHelpers.Extensions;

namespace SimpleHelpers.Tools
{
    public class NumberAnalyzer
    {
        #region Contructors

        public NumberAnalyzer(string number)
        {
            Analyze(number);
        }

        #endregion

        #region Properties

        public bool IsNumber { get; private set; }
        public bool HasScientificPart { get; private set; }
        public bool HasLeadingSign { get; private set; }
        public bool HasNoIntegerPart { get; private set; }
        public Dictionary<char, int> SpecialCharacters { get; private set; }
        public char DecimalSeparator { get; private set; }
        public char GroupSeparator { get; private set; }
        public double Result { get; private set; }

        #endregion

        #region Methods

        private void Analyze(string number)
        {
            IsNumber = IsDouble(number);
            if (!IsNumber)
                throw new Exception($"The format is not valid: '{number}'");

            string copy = number;
            CultureInfo invariantCulture = CultureInfo.InvariantCulture;

            SpecialCharacters = GetSpecialCharacters(copy)
                .ToCharArray()
                .ToDictionaryCount();
            int count = SpecialCharacters.Count();

            DecimalSeparator = default(char);
            GroupSeparator = default(char);

            if (count == 0)
            {
                NumberFormatInfo numberFormatInfo = invariantCulture.NumberFormat;
                GroupSeparator = numberFormatInfo.NumberGroupSeparator.ToCharArray().First();
                DecimalSeparator = numberFormatInfo.NumberDecimalSeparator.ToCharArray().First();
            }
            else if (count == 1)
            {
                KeyValuePair<char, int> first = SpecialCharacters.First();
                if (first.Value == 1)
                {
                    DecimalSeparator = first.Key;
                    GroupSeparator = DecimalSeparator == '.' ? ',' : '.';
                }
                else if (first.Value >= 2)
                {
                    GroupSeparator = first.Key;
                    DecimalSeparator = GroupSeparator != '.' ? '.' : ',';
                }
            }
            else if (count == 2)
            {
                GroupSeparator = SpecialCharacters.First().Key;
                DecimalSeparator = SpecialCharacters.Last().Key;
            }

            // create a temporary CultureInfo object to use when you parse.
            var ci = (CultureInfo) invariantCulture.Clone();
            ci.NumberFormat.NumberDecimalSeparator = DecimalSeparator.ToString();
            ci.NumberFormat.NumberGroupSeparator = GroupSeparator.ToString();

            Result = double.Parse(number, ci);
        }

        private string GetSpecialCharacters(string copy)
        {
            HasScientificPart = ContainsScientificPart(copy);
            if (HasScientificPart)
                copy = new Regex(@"[Ee][+-]?[0-9]+$").Replace(copy, "");

            HasLeadingSign = ContainsLeadingSign(copy);
            if (HasLeadingSign)
                copy = new Regex(@"^[+-]").Replace(copy, "");

            HasNoIntegerPart = ContainsNoIntegerPart(copy);
            if (HasNoIntegerPart)
                copy = "0" + copy;

            string specialCharacters = copy.RemoveAny("0123456789");
            return specialCharacters;
        }


        private bool IsDouble(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input));

            var doublePattern = new Regex(@"^[-+]?([0-9]*[ \.,]?)+[\.,]?[0-9]+([eE][-+]?[0-9]+)?$");
            bool isMatch = doublePattern.IsMatch(input);
            return isMatch;
        }

        private bool ContainsScientificPart(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input));

            var scientificPartPattern = new Regex(@"[Ee][+-]?[0-9]+$");
            bool isMatch = scientificPartPattern.IsMatch(input);
            return isMatch;
        }

        private bool ContainsLeadingSign(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input));

            var leadingSignPattern = new Regex(@"^[+-]");
            bool isMatch = leadingSignPattern.IsMatch(input);
            return isMatch;
        }

        private bool ContainsNoIntegerPart(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input));

            var noIntegerPartPattern = new Regex(@"^[,.][0-9]+");
            bool isMatch = noIntegerPartPattern.IsMatch(input);
            return isMatch;
        }

        public override string ToString()
        {
            return
                $"IsNumber: {IsNumber}, DecimalSeparator: {DecimalSeparator}, GroupSeparator: {GroupSeparator}, Result: {Result}";
        }

        #endregion
    }
}