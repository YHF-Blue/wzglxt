using DataEntity.Entities;
using Model.DepartmentDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace IService
{
   public  interface IDepartmentService
    {
        /// <summary>
        /// 获取部门信息用于添加用户
        /// </summary>
        /// <returns></returns>
        public List<DepartmentOutput> GetDepartments();

        /// <summary>
        /// 分页获取所有部门信息
        /// </summary>
        /// <returns></returns>
        public List<DepartmentOutput> GeDepartmentListPages(int page, int limit, out int totalCount, string selectInfo);


        /// <summary>
        /// 判断部门是否存在，存在返回true
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool CheckRepeat(string value);


        /// <summary>
        /// 添加部门信息
        /// </summary>
        /// <param name="inputEntity"></param>
        /// <returns></returns>
        bool Add(DepartmentInfo inputEntity);

        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="inputEntity"></param>
        /// <returns></returns>
        bool Update(DepartmentInfo inputEntity);

        /// <summary>
        /// 获取部门主管
        /// </summary>
        /// <returns></returns>
        List<DepartmentOutput> GetDepartmentDirector();

        /// <summary>
        /// 获取父部门
        /// </summary>
        /// <returns></returns>
        List<DepartmentOutput> GetBossDepartment();
        /// <summary>
        /// 根据ID获取部门信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DepartmentOutput GetDepartmentById(int id);

    }
}
