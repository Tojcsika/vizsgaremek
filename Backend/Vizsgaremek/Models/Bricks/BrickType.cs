using Vizsgaremek.Entities;
using System.Text.Json.Serialization;

namespace Vizsgaremek.Models.Bricks
{
    public class BrickType
    {
        public BrickType(BrickType brick)
        {
            DesignId = brick.DesignId;
            Name = brick.Name;
            Width = brick.Width;
            Height = brick.Height;
            Length = brick.Length;
            WidthSize = brick.WidthSize;
            HeightSize = brick.HeightSize;
            LengthSize = brick.LengthSize;
        }

        public BrickType(BrickEntity brick)
        {
            DesignId = brick.DesignId;
            Name = brick.Name;
            Width = brick.Width;
            Height = brick.Height;
            Length = brick.Length;
            WidthSize = brick.WidthSize;
            HeightSize = brick.HeightSize;
            LengthSize = brick.LengthSize;
        }

        [JsonConstructor]
        public BrickType() { }

        public int DesignId { get; set; }
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Length { get; set; }
        public float WidthSize { get; set; }
        public float HeightSize { get; set; }
        public float LengthSize { get; set; }
    }
}
