<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true" CodeFile="TalukaMaster.aspx.cs" Inherits="Admin_TalukaMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div class="col-md-12" style="height: 100px">
                <div class="block">
                    <div class="block-title">
                        <h2>District Master</h2>
                        <span style="float: right;">
                            <asp:HiddenField ID="HfId" runat="server" />
                            <asp:Label ID="lblmsg" runat="server" CssClass="label-display"></asp:Label>
                        </span>
                    </div>
                    <div class="form-horizontal form-bordered">
                        <div class="form-group" style="border-bottom: none;">
                             <div class="col-md-12">
                             <label class="control-label col-md-2">
                                       Select State
                                    </label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                       >
                                    </asp:DropDownList>
                                     <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="ddlState" ForeColor="Red" InitialValue="0">Please Select State</asp:RequiredFieldValidator>
                                </div>
                                 </div>
                             <div class="col-md-12">
                             <label class="control-label col-md-2">
                                      Select District
                                    </label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                     <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="ddlDistrict" ForeColor="Red" InitialValue="0">Please Select District</asp:RequiredFieldValidator>
                                </div>
                                 </div>
                            <div class="col-md-12">
                                <label class="control-label col-md-2">
                                    Taluka
                                </label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtTaluka" placeholder="Enter Taluka" 
                                        CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="txtTaluka" ForeColor="Red">Please Taluka</asp:RequiredFieldValidator>
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

                                            <asp:TemplateField HeaderText="Sr. No."  ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="State" ItemStyle-CssClass="gridText-left">
                                                <ItemTemplate>
                                                    <asp:Label CssClass="me" ID="lblState" runat="server" Text='<%#Bind("State")%>'></asp:Label>
                                                    <asp:HiddenField ID="HfId" runat="server" Value='<%#Bind("TalukaId") %>' />
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

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" AlternateText="Edit" CommandName="Updates"
                                                        CommandArgument='<%#Bind("TalukaId")%>' CausesValidation="false" data-toggle="tooltip"
                                                        ToolTip="Edit" class="btn btn-xs btn-default">
                                        <i class="fa fa-pencil" style="line-height:inherit;"></i>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnDel" runat="server" AlternateText="Delete" CommandName="deletes"
                                                        CommandArgument='<%#Bind("TalukaId")%>' OnClientClick="return confirm('Are you sure you want to delete this Record?');"
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

