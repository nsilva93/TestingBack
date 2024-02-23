//--------------------------------------------------------------------------------------
// < copyright file = "Reply.cs" company = "IEEG" >
//Copyright(c) IEEG Instituto Electoral del Estado de Guanajuato. All rights reserved. 
// </copyright> 
//--------------------------------------------------------------------------------------

namespace TestingBack.CORE.Models
{
    public class Reply
    {
        public int? totalRecords { get; set; }
        public int? currentPage  { get; set; }
        public int? pages        { get; set; }
        public object? data      { get; set; }
        public string? message   { get; set; }
        public int     result    { get; set; }
        public Reply() => this.result = 0;
    }
}
