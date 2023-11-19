using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using DataAccessLayer;
using System.Data;

namespace WebApp.Forms
{
    public partial class EmployeesList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        protected void gridViewEmployee_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortColumn = e.SortExpression;
            string sortOrder = "ASC";

            string lastSortColumn = ViewState["SortColumn"] as string;

            if (lastSortColumn == sortColumn)
            {
                string lastSortOrder = ViewState["SortOrder"] as string;
                if (lastSortOrder == "ASC")
                {
                    sortOrder = "DESC";
                }
            }

            ViewState["SortColumn"] = sortColumn;
            ViewState["SortOrder"] = sortOrder;

            ViewState["CurrentPageIndex"] = 0;
            BindGridView();
        }

        protected void gridViewEmployee_PreRender(object sender, EventArgs e)
        {
            if (gridViewEmployee.BottomPagerRow != null)
            {
                gridViewEmployee.BottomPagerRow.Visible = true;
            }
        }

        protected void PageButton_OnCommand(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Page")
            {
                int pagesCount = Convert.ToInt32(ViewState["PagesCount"]);
                int currentPageIndex = Convert.ToInt32(ViewState["CurrentPageIndex"]);

                if (pagesCount == 0)
                {
                    return;
                }

                switch (e.CommandArgument.ToString())
                {
                    case "First":
                        if (currentPageIndex > 0)
                        {
                            gridViewEmployee.PageIndex = 0;
                        }
                        else
                        {
                            return;
                        }
                        break;

                    case "Prev":
                        if (currentPageIndex > 0)
                        {
                            gridViewEmployee.PageIndex = currentPageIndex - 1;
                        }
                        else
                        {
                            return;
                        }
                        break;

                    case "Next":
                        if (currentPageIndex < pagesCount - 1)
                        {
                            gridViewEmployee.PageIndex = currentPageIndex + 1;
                        }
                        else
                        {
                            return;
                        }
                        break;

                    case "Last":
                        if (currentPageIndex < pagesCount - 1)
                        {
                            gridViewEmployee.PageIndex = pagesCount - 1;
                        }
                        else
                        {
                            return;
                        }
                        break;
                }

                ViewState["CurrentPageIndex"] = gridViewEmployee.PageIndex;
                BindGridView();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)((WebControl)sender).Parent.Parent;
                int id = Convert.ToInt32(gridViewEmployee.DataKeys[row.RowIndex].Values["Id"].ToString());

                Employee.Delete(id);
                gridViewEmployee.PageIndex = Convert.ToInt32(ViewState["CurrentPageIndex"]);
                BindGridView();

                lblMessage.CssClass = "successMessage";
                lblMessage.Text = "Record deleted successfully.";
            }
            catch (Exception)
            {
                lblMessage.CssClass = "errorMessage";
                lblMessage.Text = "Unable to delete record.";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ViewState["CurrentPageIndex"] = 0;
            BindGridView();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ViewState["CurrentPageIndex"] = 0;
            txtFirstName.Text = String.Empty;
            txtLastName.Text = String.Empty;
            BindGridView();
        }

        private void BindGridView()
        {
            try
            {
                String firstName = String.IsNullOrWhiteSpace(txtFirstName.Text) ? null : txtFirstName.Text;
                String lastName = String.IsNullOrWhiteSpace(txtLastName.Text) ? null : txtLastName.Text;
                int totalRecordsCount;
                gridViewEmployee.DataSource = Employee.GetAll(gridViewEmployee.PageIndex + 1, gridViewEmployee.PageSize, out totalRecordsCount, GetSortColumn(), GetSortOrder(), firstName, lastName);
                int pagesCount = (int)Math.Ceiling((double)totalRecordsCount / gridViewEmployee.PageSize);

                if (gridViewEmployee.PageIndex + 1 > pagesCount && pagesCount > 0)
                {
                    gridViewEmployee.PageIndex = pagesCount - 1;
                    ViewState["CurrentPageIndex"] = pagesCount - 1;
                }

                int pageIndex = gridViewEmployee.PageIndex; // Get page index before binding.
                gridViewEmployee.DataBind();
                ViewState["PagesCount"] = pagesCount;

                if (gridViewEmployee.BottomPagerRow != null)
                {
                    Label lblPage = (Label)gridViewEmployee.BottomPagerRow.FindControl("lblPage");
                    lblPage.Text = String.Format("Page {0} of {1}", pageIndex + 1, pagesCount);
                }
            }
            catch (Exception)
            {
                lblMessage.CssClass = "errorMessage";
                lblMessage.Text = "Unable to load data.";
            }
        }

        private String GetSortColumn()
        {
            return ViewState["SortColumn"] != null ? ViewState["SortColumn"].ToString() : "Id";
        }

        private String GetSortOrder()
        {
            return ViewState["SortOrder"] != null ? ViewState["SortOrder"].ToString() : "ASC";
        }
    }
}