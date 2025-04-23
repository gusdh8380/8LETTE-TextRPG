namespace _8LETTE_TextRPG.ItemFolder
{
    public interface IEquipable
    {
        EquipmentType EquipmentType { get; set; }
        bool IsEquipped { get; set; }

        void Equip();
        void Unequip();
    }
}
