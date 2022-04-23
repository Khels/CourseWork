using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseWork
{
    class DealTypeConfiguration: IEntityTypeConfiguration<DealType>
    {
        public void Configure(EntityTypeBuilder<DealType> builder)
        {
            // конфигурируем ключ с Fluent API методом HasKey()
            builder.HasKey(d => d.Id);
        }
    }
}
