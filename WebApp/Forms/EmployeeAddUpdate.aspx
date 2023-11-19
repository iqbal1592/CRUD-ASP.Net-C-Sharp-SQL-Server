<%@ Page Title="Employee Information" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="EmployeeAddUpdate.aspx.cs" Inherits="WebApp.Forms.EmployeeAddUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="AddUpdateFormUpdatePanel" runat="server">
        <ContentTemplate>
            <table class="addUpdateForm">
                <tr>
                    <td colspan="2" class="formHeader">
                        <asp:Localize ID="EmployeeInformation" Text="Employee Information" runat="server"></asp:Localize>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        <asp:ValidationSummary runat="server" DisplayMode="BulletList"
                            ValidationGroup="EmployeeInfo" CssClass="errorMessage" />
                    </td>
                </tr>
                <tr>
                    <td class="fieldLabel">
                        <asp:Localize Text="First Name" runat="server"></asp:Localize>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFirstName" MaxLength="50" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fieldLabel">
                        <asp:Localize Text="Middle Name" runat="server"></asp:Localize>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMiddleName" MaxLength="50" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fieldLabel">
                        <asp:Localize Text="Last Name" runat="server"></asp:Localize>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLastName" MaxLength="50" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fieldLabel">
                        <asp:Localize Text="Email Address" runat="server"></asp:Localize>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmailAddress" MaxLength="100" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fieldLabel"></td>
                    <td>
                        <asp:CheckBox runat="server" ID="chkIsActive" Text="Active" Checked="true" />
                    </td>
                </tr>
                <tr>
                    <td class="fieldLabel">
                        <asp:Localize Text="Employee Type" runat="server"></asp:Localize>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlEmployeeType" AppendDataBoundItems="true" runat="server">
                            <asp:ListItem Value="-1" Text="Select"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="fieldLabel">
                        <asp:Localize Text="Designation" runat="server"></asp:Localize>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDesignation" runat="server" AppendDataBoundItems="true">
                            <asp:ListItem Value="-1" Text="None"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="fieldLabel">
                        <asp:Localize Text="Country" runat="server"></asp:Localize>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCountry" runat="server" AppendDataBoundItems="true">
                            <asp:ListItem Value="-1" Text="None"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="fieldLabel">
                        <asp:Localize Text="Gender" runat="server"></asp:Localize>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlGender" runat="server">
                            <asp:ListItem Value="-1" Text="None"></asp:ListItem>
                            <asp:ListItem Value="Male" Text="Male"></asp:ListItem>
                            <asp:ListItem Value="Female" Text="Female"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="fieldLabel">
                        <asp:Localize Text="Address" runat="server"></asp:Localize>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAddress" MaxLength="200" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="fieldLabel">
                        <asp:Localize Text="Date of Birth" runat="server"></asp:Localize>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDateOfBirth" MaxLength="10" runat="server"></asp:TextBox> (e.g. 03/25/1999)
                    </td>
                </tr>
                <tr>
                    <td class="fieldLabel">
                        <asp:Localize Text="Passport No" runat="server"></asp:Localize>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassportNo" MaxLength="50" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnSave" CssClass="button" Text="Save" runat="server" ValidationGroup="EmployeeInfo"
                            OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" CssClass="button" Text="Cancel" runat="server" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFirstName" SetFocusOnError="true"
                ErrorMessage="Please enter First Name." Display="None" ValidationGroup="EmployeeInfo"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLastName" SetFocusOnError="true"
                ErrorMessage="Please enter Last Name." Display="None" ValidationGroup="EmployeeInfo"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator runat="server" 
                ControlToValidate="txtEmailAddress" SetFocusOnError="true"
                ErrorMessage="Please enter a valid Email Address" Display="None" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                ValidationGroup="EmployeeInfo"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlEmployeeType" InitialValue="-1"
                SetFocusOnError="true" ErrorMessage="Please select Employee Type." Display="None"
                ValidationGroup="EmployeeInfo"></asp:RequiredFieldValidator>
            <asp:CustomValidator runat="server" ControlToValidate="txtDateOfBirth" Display="None"
                SetFocusOnError="true" ValidationGroup="EmployeeInfo" OnServerValidate="ValidateDate"
                ErrorMessage="Please enter a valid value for Date of Birth.">
            </asp:CustomValidator>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
