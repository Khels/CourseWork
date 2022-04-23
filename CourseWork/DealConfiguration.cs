using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseWork
{
    class DealConfiguration: IEntityTypeConfiguration<Deal>
    {
        public void Configure(EntityTypeBuilder<Deal> builder)
        {
            // конфигурируем ключ с Fluent API методом HasKey()
            builder.HasKey(d => d.Id);
        }
    }
}
