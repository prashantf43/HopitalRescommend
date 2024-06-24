<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true" CodeFile="HospitalRegistration.aspx.cs" Inherits="Admin_HospitalRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div class="col-md-12" style="height: 100px">
                <div class="block">
                    <div class="block-title">
                        <h2>Hospital Registration With Basic Detail</h2>
                        <span style="float: right;">
                            <asp:HiddenField ID="HfId" runat="server" />
                            <asp:Label ID="lblmsg" runat="server" CssClass="label-display"></asp:Label>
                        </span>
                    </div>
                    <div class="form-horizontal form-bordered">
                        <div class="form-group" style="border-bottom: none;">
                            <div class="col-md-12">
                                <label class="control-label col-md-2">
                                    Hospital Name
                                </label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtHospitalName" placeholder="Enter Hospital Name"
                                        CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="txtHospitalName" ForeColor="Red">Please Hospital Name</asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <label class="control-label col-md-2">
                                    Hospital Email
                                </label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtHospitalEmail" placeholder="Enter Hospital Email"
                                        CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="txtHospitalEmail" ForeColor="Red">Please Hospital Email</asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <label class="control-label col-md-2">
                                    Phone Number
                                </label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtPhoneNumber" placeholder="Enter Phone Number" MaxLength="10"
                                        CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="txtPhoneNumber" ForeColor="Red">Please Phone Number</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revMobNo" runat="server" ErrorMessage="Invalid Phone Number."
                                        ValidationExpression="^\d+$" ControlToValidate="txtPhoneNumber"
                                        ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <label class="control-label col-md-2">
                                    Password
                                </label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtPasswords" placeholder="Enter Password"
                                        CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator4" runat="server"
                                        ControlToValidate="txtPasswords" ForeColor="Red">Please Password</asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <label class="control-label col-md-2">
                                    Select State
                                </label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator5" runat="server"
                                        ControlToValidate="ddlState" ForeColor="Red" InitialValue="0">Please Select State</asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <label class="control-label col-md-2">
                                    Select District
                                </label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator6" runat="server"
                                        ControlToValidate="ddlDistrict" ForeColor="Red" InitialValue="0">Please Select District</asp:RequiredFieldValidator>
                                </div>
                            </div>


                            <div class="col-md-12">
                                <label class="control-label col-md-2">
                                    Select Taluka
                                </label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlTaluka" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlTaluka_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator7" runat="server"
                                        ControlToValidate="ddlTaluka" ForeColor="Red" InitialValue="0">Please Select Taluka</asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <label class="control-label col-md-2">
                                    Select City
                                </label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator8" runat="server"
                                        ControlToValidate="ddlCity" ForeColor="Red" InitialValue="0">Please Select City</asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <label class="control-label col-md-2">
                                    Address
                                </label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtAddress" placeholder="Enter Address"
                                        CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator9" runat="server"
                                        ControlToValidate="txtAddress" ForeColor="Red">Please Address</asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="col-md-5">
                                    <asp:Button Text="Save" ID="BtnSave" CssClass="btn btn-sm btn-primary" runat="server" OnClick="BtnSave_Click" />
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
                                <div class="col-md-8">
                                    <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                                        CssClass="table table-vcenter table-condensed table-bordered dataTable grid-data"
                                        PageSize="10"
                                        CellPadding="9" OnRowCommand="gvDetails_RowCommand" OnPageIndexChanging="gvDetails_PageIndexChanging">
                                        <PagerStyle CssClass="pagination pagination-sm remove-margin" />
                                        <Columns>

                                            <asp:TemplateField HeaderText="Sr. No." ItemStyle-CssClass="gridText-left">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="HospitalName" ItemStyle-CssClass="gridText-left">
                                                <ItemTemplate>
                                                    <asp:Label CssClass="me" ID="lblHospitalName" runat="server" Text='<%#Bind("HospitalName")%>'></asp:Label>
                                                    <asp:HiddenField ID="HfHospitalId" runat="server" Value='<%#Bind("HospitalId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Email" ItemStyle-CssClass="gridText-left">
                                                <ItemTemplate>
                                                    <asp:Label CssClass="me" ID="lblHospitalEmail" runat="server" Text='<%#Bind("HospitalEmail")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Phone Number" ItemStyle-CssClass="gridText-left">
                                                <ItemTemplate>
                                                    <asp:Label CssClass="me" ID="lblPhoneNumber" runat="server" Text='<%#Bind("PhoneNumber")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Password" ItemStyle-CssClass="gridText-left">
                                                <ItemTemplate>
                                                    <asp:Label CssClass="me" ID="lblPasswords" runat="server" Text='<%#Bind("Passwords")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="State" ItemStyle-CssClass="gridText-left">
                                                <ItemTemplate>
                                                    <asp:Label CssClass="me" ID="lblState" runat="server" Text='<%#Bind("State")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="District" ItemStyle-CssClass="gridText-left">
                                                <ItemTemplate>
                                                    <asp:Label CssClass="me" ID="lblDistrict" runat="server" Text='<%#Bind("District")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Taluka" ItemStyle-CssClass="gridText-left">
                                                <ItemTemplate>
                                                    <asp:Label CssClass="me" ID="lblTaluka" runat="server" Text='<%#Bind("Taluka")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="City/Area" ItemStyle-CssClass="gridText-left">
                                                <ItemTemplate>
                                                    <asp:Label CssClass="me" ID="lblCity" runat="server" Text='<%#Bind("City")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address" ItemStyle-CssClass="gridText-left">
                                                <ItemTemplate>
                                                    <asp:Label CssClass="me" ID="lblAddress" runat="server" Text='<%#Bind("Address")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" AlternateText="Edit" CommandName="Updates"
                                                        CommandArgument='<%#Bind("HospitalId")%>' CausesValidation="false" data-toggle="tooltip"
                                                        ToolTip="Edit" class="btn btn-xs btn-default">
                                        <i class="fa fa-pencil" style="line-height:inherit;"></i>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnDel" runat="server" AlternateText="Delete" CommandName="deletes"
                                                        CommandArgument='<%#Bind("HospitalId")%>' OnClientClick="return confirm('Are you sure you want to delete this Record?');"
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

