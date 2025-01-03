using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;


    public class SurgeryRoomConfiguration : IEntityTypeConfiguration<SurgeryRoom>
    {
        public void Configure(EntityTypeBuilder<SurgeryRoom> builder)
        {
            builder.ToTable("SurgeryRooms");

            // Definir a chave primária
            builder.HasKey(r => r.Id);

            // Conversores para propriedades específicas
            var roomNumberConverter = new ValueConverter<RoomNumber, string>(
                id => id.AsString(),
                value => new RoomNumber(value)
            );

            var capacityConverter = new ValueConverter<Capacity, int>(
                capacity => capacity.capacity,
                value => new Capacity(value)
            );

            var maintenanceSlotsConverter = new ValueConverter<MaintenanceSlots, int>(
                slots => slots.maintenanceSlots,
                value => new MaintenanceSlots(value)
            );

            var roomNameConverter = new ValueConverter<RoomName, string>(
                name => name.roomName,
                value => new RoomName(value)
            );

            var roomTypeIdConverter = new ValueConverter<RoomTypeId, string>(
                id => id.AsString(),
                value => new RoomTypeId(value)
            );

            // Configurações das propriedades
            builder.Property(r => r.Id)
                .IsRequired()
                .HasConversion(roomNumberConverter);

            builder.Property(r => r.Capacity)
                .IsRequired()
                .HasConversion(capacityConverter);

            builder.Property(r => r.MaintenanceSlots)
                .IsRequired()
                .HasConversion(maintenanceSlotsConverter);

            builder.Property(r => r.RoomName)
                .IsRequired()
                .HasConversion(roomNameConverter);

            builder.Property(r => r.RoomTypeId)
                .IsRequired()
                .HasConversion(roomTypeIdConverter);

            builder.Property(r => r.RoomStatus)
                .IsRequired()
                .HasConversion<int>(); // Enum armazenado como inteiro
        }
    }

