using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace quanlysinhvien.DTO
{
    internal class Lop
    {
        private int _iD;
        private string _tenLop;
        public Lop() { }
        public Lop(DataRow item)
        {
            this.ID = int.Parse(item["ID"].ToString());
            this.TenLop = item["TenLop"].ToString();
        }

        public int ID { get => _iD; set => _iD = value; }
        public string TenLop { get => _tenLop; set => _tenLop = value; }
    }
}

