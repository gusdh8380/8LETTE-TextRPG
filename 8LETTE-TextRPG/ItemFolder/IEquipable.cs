namespace _8LETTE_TextRPG.ItemFolder
{
    public interface IEquipable
    {
        float EquipAtkInc { get; set; }
        float EquipDefInc { get; set; }
        float EquipHpInc { get; set; }
        EquipmentType EquipmentType { get; set; }
        bool IsEquipped { get; set; }

        void Equip();
        void Unequip();
    }
}
