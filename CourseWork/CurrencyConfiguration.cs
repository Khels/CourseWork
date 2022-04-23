using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseWork
{
    public class CurrencyConfiguration: IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            // переопределение таблицы Currency с помощью метода ToTable() Fluent API
            builder.ToTable("CurrencyTable");

            // ограничение свойства с помощью метода IsRequired()
            builder.Property(ac => ac.CurrencyFull).IsRequired();

            // добавляем явную типизацию свойства с помощью метода HasColumnType()
            builder.Property(ac => ac.CurrencyShort).IsRequired().HasColumnType("varchar(3)");
        }
    }
}
