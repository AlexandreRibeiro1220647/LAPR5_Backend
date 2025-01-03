using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
    
    public class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> builder)
        {
            builder.ToTable("RoomTypes");

            // Definir a chave primária
            builder.HasKey(r => r.Id);

            // Conversores para propriedades específicas
            var roomTypeIdConverter = new ValueConverter<RoomTypeId, string>(
                id => id.AsString(),
                value => new RoomTypeId(value)
            );

            var roomDesignationConverter = new ValueConverter<RoomDesignation, string>(
                designation => designation.roomDesignation,
                value => new RoomDesignation(value)
            );

            var roomDescriptionConverter = new ValueConverter<RoomDescription, string>(
                description => description.roomDescription,
                value => new RoomDescription(value)
            );

            // Configurações das propriedades
            builder.Property(r => r.Id)
                .IsRequired()
                .HasConversion(roomTypeIdConverter);

            builder.Property(r => r.RoomDesignation)
                .IsRequired()
                .HasConversion(roomDesignationConverter);

            builder.Property(r => r.RoomDescription)
                .IsRequired()
                .HasConversion(roomDescriptionConverter);
        }
    }