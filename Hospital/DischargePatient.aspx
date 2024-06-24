<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true" CodeFile="DischargePatient.aspx.cs" Inherits="Hospital_DischargePatient" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div class="col-md-12" style="height: 100px">
                <div class="block">
                    <div class="block-title">
                        <h2>Discharge Patient</h2>
                        <span style="float: right;">
                            <asp:HiddenField ID="HfId" runat="server" />
                            <asp:HiddenField ID="hfPatientIds" runat="server" />
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
                                    <asp:TextBox ID="txtAadharNumber" placeholder="Enter Aadhar Number" MaxLength="12" BackColor="#ffff99"
                                        CssClass="form-control" runat="server" ValidationGroup="g1"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        ControlToValidate="txtAadharNumber" ErrorMessage="Please Enter Only Numbers" ForeColor="Red"
                                        ValidationExpression="^\d+$" ValidationGroup="g1"></asp:RegularExpressionValidator>
                                    <br />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                        ControlToValidate="txtAadharNumber" ErrorMessage="Please Enter 12 Number" ForeColor="Red"
                                        ValidationExpression="[0-9]{12}" ValidationGroup="g1"></asp:RegularExpressionValidator>
                                    <br />
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="txtAadharNumber" ValidationGroup="g1" ForeColor="Red">Please Enter Aadhar Number</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-4">
                                    <asp:Button Text="Search" ID="btnSearch" ValidationGroup="g1" CssClass="btn btn-sm btn-primary" runat="server" OnClick="btnSearch_Click" />
                                </div>
                            </div>

                            <div class="col-md-12">
                                <label class="control-label col-md-2">
                                    Name
                                </label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtName"
                                        CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>

                                </div>
                            </div>

                            <div class="col-md-12">
                                <label class="control-label col-md-2">
                                    Address
                                </label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtAddress"
                                        CssClass="form-control" runat="server" ReadOnly="true" TextMode="MultiLine"></asp:TextBox>

                                </div>
                            </div>




                            <div class="col-md-12">
                                <label class="control-label col-md-2">
                                    Date Of Admission
                                </label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtDateOfAdmission"
                                        CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>

                                </div>
                            </div>

                            <div class="col-md-12">
                                <label class="control-label col-md-2">
                                    Patient Status
                                </label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlPatientStatus" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="ddlPatientStatus" ForeColor="Red" InitialValue="0">Please Select Patient Status</asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <label class="control-label col-md-2">
                                    Discharge Date 
                                </label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtDischargeDate" placeholder="Enter Discharge Date "
                                        CssClass="form-control" runat="server" AutoPostBack="True" OnTextChanged="txtDischargeDate_TextChanged"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="txtDischargeDate" ForeColor="Red">Please Enter Discharge Date </asp:RequiredFieldValidator>

                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDischargeDate" Format="dd-MM-yyyy"></asp:CalendarExtender>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <label class="control-label col-md-2">
                                    Total days required to clear
                                </label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtTotalClearDays"
                                        CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                              <div class="col-md-12">
                                <label class="control-label col-md-2">
                                   Select Room Type
                                </label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlRoomType" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlRoomType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator4" runat="server"
                                        ControlToValidate="ddlRoomType" ForeColor="Red" InitialValue="0">Please Select Room Type</asp:RequiredFieldValidator>
                                </div>
                                  <div class="col-md-4">
                                        <asp:TextBox ID="txtRentPerDay" placeholder="Rent Per Day"
                                        CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                      </div>
                            </div>

                              <div class="col-md-12">
                                <label class="control-label col-md-2">
                                   Total Rent Cost
                                </label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtRentCost"
                                        CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                              <div class="col-md-12">
                                <label class="control-label col-md-2">
                                    Treatment Cost
                                </label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtTreatmentCost"
                                        CssClass="form-control" runat="server" AutoPostBack="True" OnTextChanged="txtTreatmentCost_TextChanged"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <label class="control-label col-md-2">
                                    Total Final Charges
                                </label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtFinalCharges" 
                                        CssClass="form-control" runat="server" Enabled="False" Text="0.00"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator6" runat="server"
                                        ControlToValidate="txtFinalCharges" ForeColor="Red">Please Enter Total Final Charges</asp:RequiredFieldValidator>
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

                                <%--  <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                                    CssClass="table table-vcenter table-condensed table-bordered dataTable grid-data"
                                    PageSize="10"
                                    CellPadding="9" OnRowCommand="gvDetails_RowCommand">
                                    <PagerStyle CssClass="pagination pagination-sm remove-margin" />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sr. No." ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date Of Admission" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label CssClass="me" ID="lblDateOfAdmission" runat="server" Text='<%#Convert.ToDateTime(Eval("DateOfAdmission")).ToString("dd-MM-yyyy")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Aadhar Number" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label CssClass="me" ID="lblAadharNumber" runat="server" Text='<%#Bind("AadharNumber")%>'></asp:Label>
                                                <asp:HiddenField ID="HfId" runat="server" Value='<%#Bind("Id") %>' />
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

                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" AlternateText="Edit" CommandName="Updates"
                                                    CommandArgument='<%#Bind("Id")%>' CausesValidation="false" data-toggle="tooltip"
                                                    ToolTip="Edit" class="btn btn-xs btn-default">
                                        <i class="fa fa-pencil" style="line-height:inherit;"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnDel" runat="server" AlternateText="Delete" CommandName="deletes"
                                                    CommandArgument='<%#Bind("Id")%>' OnClientClick="return confirm('Are you sure you want to delete this Record?');"
                                                    CausesValidation="false" data-toggle="tooltip" ToolTip="Delete" class="btn btn-xs btn-danger">
                                        <i class="fa fa-times" style="line-height:inherit;"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

