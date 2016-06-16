using System;

namespace FallDownDemo
{
	public class HighScore : IComparable {
		private int _score;
		private string _name;
		private DateTime _date;
			
		public string Name {
			get { return _name; }
			set { _name = value; }
		}

		public int Score {
			get { return _score; }
			set { _score = value; }
		}

		public DateTime Date {
			get { return _date; }
			set { _date = value; }
		}

		public HighScore(string name, int score, DateTime date)
		{
			Name = name;
			Score = score;
			Date = date;
		}

		public int CompareTo(object scoreObject)
		{
			HighScore compareScore = (HighScore)scoreObject;
			if (this.Score > compareScore.Score)
				return 1;
			else if(this.Score == compareScore.Score)
            {   // Date is tie breaker
                if (this.Date > compareScore.Date)
                    return -1;
                else if (this.Date < compareScore.Date)
                    return 1;
                else
                    return 0;
            }
			else
				return -1;
		}
	}
}