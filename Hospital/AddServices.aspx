<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true" CodeFile="AddServices.aspx.cs" Inherits="Hospital_AddServices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div class="col-md-12" style="height: 100px">
                <div class="block">
                    <div class="block-title">
                        <h2>Add Services</h2>
                        <span style="float: right;">
                            <asp:HiddenField ID="HfId" runat="server" />
                            <asp:Label ID="lblmsg" runat="server" CssClass="label-display"></asp:Label>
                        </span>
                    </div>
                    <div class="form-horizontal form-bordered">
                        <div class="form-group" style="border-bottom: none;">
                            <div class="col-md-12">
                                <label class="control-label col-md-2">
                                    Service
                                </label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtService" placeholder="Enter Service" MaxLength="150"
                                        CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="txtService" ForeColor="Red">Please Service</asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <label class="control-label col-md-2">
                                    Service Detail
                                </label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtServiceDetails" placeholder="Enter Service Detail"
                                        CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="txtServiceDetails" ForeColor="Red">Please Service Detail</asp:RequiredFieldValidator>
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
                                        CellPadding="9" OnRowCommand="gvDetails_RowCommand">
                                        <PagerStyle CssClass="pagination pagination-sm remove-margin" />
                                        <Columns>

                                            <asp:TemplateField HeaderText="Sr. No." ItemStyle-CssClass="gridText-left">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Service" ItemStyle-CssClass="gridText-left">
                                                <ItemTemplate>
                                                    <asp:Label CssClass="me" ID="lblCollegeCode" runat="server" Text='<%#Bind("ServiceName")%>'></asp:Label>
                                                    <asp:HiddenField ID="HfServiceId" runat="server" Value='<%#Bind("ServiceId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Service Detail" ItemStyle-CssClass="gridText-left">
                                                <ItemTemplate>
                                                    <asp:Label CssClass="me" ID="lblCollegeName" runat="server" Text='<%#Bind("ServiceDetail")%>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" AlternateText="Edit" CommandName="Updates"
                                                        CommandArgument='<%#Bind("ServiceId")%>' CausesValidation="false" data-toggle="tooltip"
                                                        ToolTip="Edit" class="btn btn-xs btn-default">
                                        <i class="fa fa-pencil" style="line-height:inherit;"></i>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnDel" runat="server" AlternateText="Delete" CommandName="deletes"
                                                        CommandArgument='<%#Bind("ServiceId")%>' OnClientClick="return confirm('Are you sure you want to delete this Record?');"
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

