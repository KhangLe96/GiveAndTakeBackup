namespace Giveaway.Data.Models.Database
{
    public class Rank : BaseEntity
    {
        public int RankStar { get; set; }
        public string Message { get; set; }

        //public virtual User Giver { get; set; }
        //public virtual User Taker { get; set; }
    }
}
