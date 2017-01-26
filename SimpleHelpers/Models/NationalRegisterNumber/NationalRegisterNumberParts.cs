using System;

namespace SimpleHelpers.Models.NationalRegisterNumber
{
    public class NationalRegisterNumberParts
    {
        private string _yy;
        private string _mm;
        private string _dd;
        private string _xxx;
        private string _cd;

        public string YY
        {
            get { return _yy; }
            private set
            {
                _yy = value;
                YYi = Convert.ToInt32(_yy);
            }
        }

        public string MM
        {
            get { return _mm; }
            set
            {
                _mm = value;
                MMi = Convert.ToInt32(_mm);
            }
        }

        public string DD
        {
            get { return _dd; }
            set
            {
                _dd = value;
                DDi = Convert.ToInt32(_dd);
            }
        }

        public string XXX
        {
            get { return _xxx; }
            set
            {
                _xxx = value;
                XXXi = Convert.ToInt32(_xxx);
            }
        }

        public string CD
        {
            get { return _cd; }
            set
            {
                _cd = value;
                CDi = Convert.ToInt32(_cd);
            }
        }

        public int YYi { get; private set; }
        public int MMi { get; private set; }
        public int DDi { get; private set; }
        public int XXXi { get; private set; }
        public int CDi { get; private set; }

        public string YYMMDD => $"{YY}{MM}{DD}";
        public string YYMMDDXXX => $"{YYMMDD}{XXX}";
        public string YYMMDDXXXCD => $"{YYMMDDXXX}{CD}";


        public NationalRegisterNumberParts(string nrn)
        {
            var isValid = NationalRegisterNumber.Validate(nrn);
            if (!isValid)
                throw new ArgumentException("NRN is not valid.", nameof(nrn));

            YY = nrn.Substring(0, 2);
            MM = nrn.Substring(2, 2);
            DD = nrn.Substring(4, 2);
            XXX = nrn.Substring(6, 3);
            CD = nrn.Substring(9, 2);
        }

        public override string ToString()
        {
            return $"{_yy}.{_mm}.{_dd}-{_xxx}.{_cd}";
        }
    }
}