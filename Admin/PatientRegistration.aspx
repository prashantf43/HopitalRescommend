<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true" CodeFile="PatientRegistration.aspx.cs" Inherits="Admin_PatientRegistration" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div class="col-md-12" style="height: 100px">
                <div class="block">
                    <div class="block-title">
                        <h2>Patient Registration</h2>
                        <span style="float: right;">
                            <asp:HiddenField ID="HfId" runat="server" />
                            <asp:Label ID="lblmsg" runat="server" CssClass="label-display"></asp:Label>
                        </span>
                    </div>
                    <div class="form-horizontal form-bordered">
                        <div class="form-group" style="border-bottom: none;">

                            <div class="col-md-12">
                                <label class="control-label col-md-2">
                                    Aadhar Number
                                </label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtAadharNumber" placeholder="Enter Aadhar Number" MaxLength="12"
                                        CssClass="form-control" runat="server" ></asp:TextBox>

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        ControlToValidate="txtAadharNumber" ErrorMessage="Please Enter Only Numbers" ForeColor="Red"
                                        ValidationExpression="^\d+$" ></asp:RegularExpressionValidator>
                                          <br />
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                        ControlToValidate="txtAadharNumber" ErrorMessage="Please Enter 12 Number" ForeColor="Red"
                                        ValidationExpression="[0-9]{12}"></asp:RegularExpressionValidator>
                                    <br />
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="txtAadharNumber"  ForeColor="Red">Please Enter Aadhar Number</asp:RequiredFieldValidator>
                                </div>
                               
                            </div>

                            <div class="col-md-12">
                                <label class="control-label col-md-2">
                                    Name
                                </label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtName"
                                        CssClass="form-control" runat="server" placeholder="Enter Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="txtName"  ForeColor="Red">Please Enter Name</asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <label class="control-label col-md-2">
                                    Address
                                </label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtAddress"
                                        CssClass="form-control" runat="server" placeholder="Enter Address" TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="txtAddress"  ForeColor="Red">Please Enter Address</asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <label class="control-label col-md-2">
                                    DOB
                                </label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtDOB"
                                        CssClass="form-control" runat="server" placeholder="Enter DOB"></asp:TextBox>
                                     <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator4" runat="server"
                                        ControlToValidate="txtDOB"  ForeColor="Red">Please Enter DOB</asp:RequiredFieldValidator>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDOB" Format="dd-MM-yyyy"></asp:CalendarExtender>

                                </div>
                            </div>


                            <div class="col-md-12">
                                <label class="control-label col-md-2">
                                    Contact Number
                                </label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtContactNumber"
                                        CssClass="form-control" runat="server" placeholder="Enter Contact Number"></asp:TextBox>
                                   <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator5" runat="server"
                                        ControlToValidate="txtContactNumber"  ForeColor="Red">Please Enter Contact Number</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                        ControlToValidate="txtContactNumber" ErrorMessage="Please Enter Only Numbers" ForeColor="Red"
                                        ValidationExpression="^\d+$" ></asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <label class="control-label col-md-2">
                                   Password
                                </label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtPassword"
                                        CssClass="form-control" runat="server" placeholder="Enter Password"></asp:TextBox>
                                   <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator6" runat="server"
                                        ControlToValidate="txtPassword"  ForeColor="Red">Please Enter Password</asp:RequiredFieldValidator>
                                   
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="col-md-5">
                                    <asp:Button Text="Save" ID="BtnSave" CssClass="btn btn-sm btn-primary" runat="server" OnClick="BtnSave_Click"  />
                                    &nbsp;&nbsp;
                                         <asp:Button ID="BtnClear" Text="Clear" runat="server" CssClass="btn btn-sm btn-danger"
                                             CausesValidation="false" OnClick="BtnClear_Click" />
                                </div>
                            </div>

                            <div style="clear: both; height: 15px;">
                            </div>
                            <div style="clear: both; height: 15px;">
                            </div>
                                <div class="col-md-12">
                               
                                    <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                                        CssClass="table table-vcenter table-condensed table-bordered dataTable grid-data"
                                        PageSize="10"
                                        CellPadding="9" OnRowCommand="gvDetails_RowCommand" OnPageIndexChanging="gvDetails_PageIndexChanging">
                                        <PagerStyle CssClass="pagination pagination-sm remove-margin" />
                                        <Columns>

                                            <asp:TemplateField HeaderText="Sr. No." ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Aadhar Number" ItemStyle-CssClass="gridText-left">
                                                <ItemTemplate>
                                                    <asp:Label CssClass="me" ID="lblAadharNumber" runat="server" Text='<%#Bind("AadharNumber")%>'></asp:Label>
                                                    <asp:HiddenField ID="HfId" runat="server" Value='<%#Bind("PatientId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Name" ItemStyle-CssClass="gridText-left">
                                                <ItemTemplate>
                                                    <asp:Label CssClass="me" ID="lblName" runat="server" Text='<%#Bind("Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                              <asp:TemplateField HeaderText="Address" ItemStyle-CssClass="gridText-left">
                                                <ItemTemplate>
                                                    <asp:Label CssClass="me" ID="lblAddress" runat="server" Text='<%#Bind("Address")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                              <asp:TemplateField HeaderText="DOB" ItemStyle-CssClass="gridText-left">
                                                <ItemTemplate>
                                                    <asp:Label CssClass="me" ID="lblDOB" runat="server" Text='<%#Convert.ToDateTime(Eval("DOB")).ToString("dd-MM-yyyy")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                              <asp:TemplateField HeaderText="Contact Number" ItemStyle-CssClass="gridText-left">
                                                <ItemTemplate>
                                                    <asp:Label CssClass="me" ID="lblContactNumber" runat="server" Text='<%#Bind("ContactNumber")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Password" ItemStyle-CssClass="gridText-left">
                                                <ItemTemplate>
                                                    <asp:Label CssClass="me" ID="lblPasswords" runat="server" Text='<%#Bind("Passwords")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" AlternateText="Edit" CommandName="Updates"
                                                        CommandArgument='<%#Bind("PatientId")%>' CausesValidation="false" data-toggle="tooltip"
                                                        ToolTip="Edit" class="btn btn-xs btn-default">
                                        <i class="fa fa-pencil" style="line-height:inherit;"></i>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnDel" runat="server" AlternateText="Delete" CommandName="deletes"
                                                        CommandArgument='<%#Bind("PatientId")%>' OnClientClick="return confirm('Are you sure you want to delete this Record?');"
                                                        CausesValidation="false" data-toggle="tooltip" ToolTip="Delete" class="btn btn-xs btn-danger">
                                        <i class="fa fa-times" style="line-height:inherit;"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

