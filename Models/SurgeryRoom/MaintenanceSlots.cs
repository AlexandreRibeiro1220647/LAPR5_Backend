public class MaintenanceSlots {
    public int maintenanceSlots { get; private set; }

    public MaintenanceSlots() {}

    public MaintenanceSlots (int maintenanceSlots) {
        this.maintenanceSlots = maintenanceSlots;
    }

    public void ChangeMaintenanceSlots(int maintenanceSlots) {
        this.maintenanceSlots = maintenanceSlots;
    }

}