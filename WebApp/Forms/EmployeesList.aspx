<%@ Page Title="Employees" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeesList.aspx.cs" Inherits="WebApp.Forms.EmployeesList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="ListUpdatePanel" runat="server">
        <ContentTemplate>
            <div class="filter">
                <div class="filterHeader">Search</div>
                <table>
                    <tr>
                        <td class="fieldLabel">
                            <asp:Localize ID="FirstName" Text="First Name" runat="server"></asp:Localize>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtFirstName"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="fieldLabel">
                            <asp:Localize ID="LastName" Text="Last Name" runat="server"></asp:Localize>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtLastName"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="btnSearch" CssClass="button" Text="Search" runat="server" OnClick="btnSearch_Click" />
                            <asp:Button ID="btnClear" CssClass="button" Text="Clear" runat="server" OnClick="btnClear_Click" />
                        </td>
                    </tr>
                </table>
            </div>

            <asp:Label runat="server" ID="lblMessage"></asp:Label>

            <asp:GridView ID="gridViewEmployee" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                CssClass="grid" AllowSorting="true" AllowPaging="true" PageSize="5"
                AlternatingRowStyle-CssClass="gridAlternateRow"
                OnSorting="gridViewEmployee_Sorting" ShowHeaderWhenEmpty="true" EmptyDataText="No data found."
                HeaderStyle-CssClass="gridHeaderRow" GridLines="None" OnPreRender="gridViewEmployee_PreRender">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="ID" Visible="False" />
                    <asp:BoundField DataField="FirstName" ItemStyle-Width="20%" HeaderText="First Name" SortExpression="FirstName" />
                    <asp:BoundField DataField="LastName" ItemStyle-Width="20%" HeaderText="Last Name" SortExpression="LastName" />
                    <asp:BoundField DataField="Gender" ItemStyle-Width="20%" HeaderText="Gender" SortExpression="Gender" />
                    <asp:BoundField DataField="EmailAddress" ItemStyle-Width="30%" HeaderText="Email Address" SortExpression="EmailAddress" />
                    <asp:TemplateField HeaderText="Active" ItemStyle-Width="10%" SortExpression="IsActive">
                        <ItemTemplate>
                            <asp:CheckBox runat="server" Enabled="false" Checked='<%# Convert.ToBoolean(Eval("IsActive")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="3%">
                        <ItemTemplate>
                            <asp:HyperLink runat="server" ImageUrl="~/Images/Edit.png" NavigateUrl='<%# "~/Forms/EmployeeAddUpdate.aspx?Id=" + Eval("Id")  %>' ToolTip="Edit"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="3%">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnDelete" runat="server" ToolTip="Delete" ImageUrl="~/Images/Delete.png" OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure you want to delete this record?');"></asp:ImageButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerTemplate>
                    <div class="pager">
                        <asp:ImageButton runat="server" ID="btnFirst" ToolTip="First" CssClass="pagerButton"
                            ImageUrl="~/Images/First.png" CommandName="Page" CommandArgument="First" OnCommand="PageButton_OnCommand" />
                        <asp:ImageButton runat="server" ID="btnPrevious" ToolTip="Previous"  CssClass="pagerButton"
                            ImageUrl="~/Images/Previous.png" CommandName="Page" CommandArgument="Prev" OnCommand="PageButton_OnCommand" />
                        <asp:Label runat="server" ID="lblPage" CssClass="pagesCount"></asp:Label>
                        <asp:ImageButton runat="server" ID="btnNext" ToolTip="Next"  CssClass="pagerButton"
                            ImageUrl="~/Images/Next.png" CommandName="Page" CommandArgument="Next" OnCommand="PageButton_OnCommand" />
                        <asp:ImageButton runat="server" ID="btnLast" ToolTip="Last"  CssClass="pagerButton"
                            ImageUrl="~/Images/Last.png" CommandName="Page" CommandArgument="Last" OnCommand="PageButton_OnCommand" />
                    </div>
                </PagerTemplate>
            </asp:GridView>

            <asp:HyperLink runat="server" CssClass="button" NavigateUrl="~/Forms/EmployeeAddUpdate.aspx">Add Employee</asp:HyperLink>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
