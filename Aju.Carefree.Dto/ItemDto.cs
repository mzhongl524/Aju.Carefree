﻿namespace Aju.Carefree.Dto
{
    public class ItemDto
    {
        public ItemDto()
        {
            ParentId = "0";
            IsEnabled = true;
        }
        public string Id { get; set; }

        public string ParentId { get; set; }

        public string FullName { get; set; }

        public string EnCode { get; set; }

        public string Remark { get; set; }

        public bool IsEnabled { get; set; }
    }
}
