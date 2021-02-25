
[System.Serializable]
public class PlayerData
{
    public int rakam_0, rakam_1, rakam_2, rakam_3, rakam_4,
                         rakam_5, rakam_6, rakam_7, rakam_8, rakam_9;
    public int yesilKeycard, sariKeycard, kirmiziKeycard;

    public int inventoryCapacity;

    public float[] position = new float[3];

    public PlayerData(instance_Player_Inventory inventory, instance_LittlePeopleController littlePeopleController)
    {
        rakam_0 = inventory.Rakam_0; rakam_1 = inventory.Rakam_1; rakam_2 = inventory.Rakam_2;
        rakam_3 = inventory.Rakam_3; rakam_4 = inventory.Rakam_4; rakam_5 = inventory.Rakam_5;
        rakam_6 = inventory.Rakam_6; rakam_7 = inventory.Rakam_7; rakam_8 = inventory.Rakam_8;
        rakam_9 = inventory.Rakam_9;
        yesilKeycard = inventory._yesilKeycard; sariKeycard = inventory._sariKeycard;
        kirmiziKeycard = inventory._kirmiziKeycard;

        inventoryCapacity = inventory.Inventory_Capacity;

        position[0] = littlePeopleController.transform.position.x;
        position[1] = littlePeopleController.transform.position.y;
        position[2] = littlePeopleController.transform.position.z;
    }
}
