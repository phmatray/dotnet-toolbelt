using System;
using System.Text.RegularExpressions;

namespace SimpleHelpers.Models.NationalRegisterNumber
{
    public class NationalRegisterNumber
    {
        #region static

        public static bool Validate(string nrn)
        {
            try
            {
                if (nrn == null)
                    throw new ArgumentNullException(nameof(nrn));
                if (nrn.Length != 11)
                    throw new ArgumentException("Length must be 11.");
                if (!Regex.IsMatch(nrn, "^[0-9]{11}$"))
                    throw new ArgumentException("This is not a valid National Register Number.");

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static int ComputeCheckNumber(string yymmddxxx, bool bornAfter2000 = false)
        {
            if (bornAfter2000)
                yymmddxxx = $"2{yymmddxxx}";

            var checkNumber = 97 - (int)(Convert.ToInt64(yymmddxxx) % 97);
            if (checkNumber == 0)
                checkNumber = 97;

            return checkNumber;
        }

        #endregion

        private readonly NationalRegisterNumberParts _nrnParts;


        /// <summary>
        /// Create a <see cref="NationalRegisterNumber"/> with all informations.
        /// </summary>
        /// <param name="nrn">The number or bisNumer</param>
        public NationalRegisterNumber(string nrn)
        {
            // informations
            // http://www.ibz.rrn.fgov.be/fileadmin/user_upload/fr/rn/instructions/liste-TI/TI000_Numerodidentification.pdf
            // https://www.ksz-bcss.fgov.be/fr/services-et-support/services/registres-bcss
            // https://www.ksz-bcss.fgov.be/sites/default/files/assets/services_et_support/cbss_manual_fr.pdf

            _nrnParts = new NationalRegisterNumberParts(nrn);
        }

        public bool IsBisNumber
            => _nrnParts.MMi >= 20;

        public string NRN
            => _nrnParts.ToString();

        public string Genre
        {
            get
            {
                // A noter qu'en cas de changement de genre, la personne reçoit un nouveau numéro national.
                // Le programme est donc toujours capable de connaître le genre d'un individu.

                if (IsBisNumber && !IsGenreKnown) // augmenté de 40 si le genre est inconnu
                    return null;

                return (_nrnParts.XXXi & 1) == 1 ? "M" : "F";
            }
        }

        public bool IsGenreKnown
            => !IsBisNumber || _nrnParts.MMi / 20 == 2;

        public string Birthdate
            => $"{_nrnParts.DD}/{_nrnParts.MMi % 20:D2}/{Century:D2}{_nrnParts.YY}";

        public BirthdateType BirthdateType
        {
            get
            {
                if (_nrnParts.YY == "00")
                    return BirthdateType.Unknown;
                if (_nrnParts.MM == "00" || _nrnParts.MM == "20" || _nrnParts.MM == "40")
                    return BirthdateType.Incomplete;

                return BirthdateType.Ok;
            }
        }

        public bool IsBornBefore2000
            => BirthdateType != BirthdateType.Unknown &&
               ComputeCheckNumber(_nrnParts.YYMMDDXXX) == _nrnParts.CDi;

        public bool IsBornAfter2000
            => BirthdateType != BirthdateType.Unknown &&
               ComputeCheckNumber(_nrnParts.YYMMDDXXX, true) == _nrnParts.CDi;

        public int Century
        {
            get
            {
                switch (BirthdateType)
                {
                    case BirthdateType.Ok:
                    case BirthdateType.Incomplete:
                        return IsBornBefore2000 ? 19 : 20;
                    case BirthdateType.Unknown:
                        return 0;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

    }
}