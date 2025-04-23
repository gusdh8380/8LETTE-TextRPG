namespace _8LETTE_TextRPG.ItemFolder
{
    public interface IUsable
    {
        float UsedAtkInc { get; set; }
        float UsedDefInc { get; set; }
        float UsedHpInc { get; set; }

        void Use();
    }
}
