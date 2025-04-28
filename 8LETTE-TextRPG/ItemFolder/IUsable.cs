namespace _8LETTE_TextRPG.ItemFolder
{
    public interface IUsable
    {
        UseType UseType { get; set; }

        void Use();
    }
}
