using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utility
{
  public   class NpoiHelper
    {
        public static List<string> InputExcel(IFormFile file)
        {
            //被返回的list集合
            List<string> list = new List<string>();
            //读取文件流的信息
            IWorkbook workbook = new XSSFWorkbook(file.OpenReadStream());
            //把文件流信息生成一张表格
            ISheet sheet = workbook.GetSheetAt(0);//表
            //开始读表格中的行数
            IRow firstRow = sheet.GetRow(0);//行
            int row = firstRow.LastCellNum;//总的列数
            int cell = sheet.LastRowNum;//总行数
            for (int j = 1; j < cell + 1; j++)
            {
                IRow ContenRow = sheet.GetRow(j);//行
                for (int i = 0; i < row; i++)
                {
                    ICell TiteCell = firstRow.GetCell(i);//单元格
                    if (i != row - 1)
                    {
                        //获取改单元格内的基本信息
                        var value = ContenRow.GetCell(i);
                        if (value != null)
                        {
                            //判断单元格内的数据类型
                            string str = value.CellType.ToString();
                            //Blank为空的意思
                            if (str != "Blank")
                            {
                                //开始拼接添加到list集合的数据字符串
                                if (str == "String")
                                {
                                    list.Add($"{TiteCell.StringCellValue}&&{value.StringCellValue}");
                                    continue;
                                }
                                else if (str == "Double")
                                {
                                    list.Add($"{TiteCell.StringCellValue}&&{value.NumericCellValue}");
                                    continue;
                                }
                                else if (str == "Numeric")
                                {
                                    list.Add($"{TiteCell.StringCellValue}&&{value.NumericCellValue}");
                                    continue;
                                }
                            }
                            //由于会出现某列的某行不进行填写，但是该数据列又必须存在，故要进行添加一次，但不赋予任何的数据意义，
                            //只是为了方便对后期对数据的处理
                            if (TiteCell.StringCellValue != "")
                            {
                                list.Add($"{TiteCell.StringCellValue}&&无");
                            }
                        }
                    }
                }
            }
            return list;
        }
    }
}
