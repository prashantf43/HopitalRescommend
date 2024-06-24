<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true" CodeFile="ViewRiews.aspx.cs" Inherits="Hospital_ViewRiews" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
      <style type="text/css">
        .Star {
            background-image: url(../images/Star.gif);
            height: 17px;
            width: 17px;
        }

        .WaitingStar {
            background-image: url(../images/WaitingStar.gif);
            height: 17px;
            width: 17px;
        }

        .FilledStar {
            background-image: url(../images/FilledStar.gif);
            height: 17px;
            width: 17px;
        }
    </style>
     <asp:UpdatePanel ID="up1" runat="server">
        <ContentTemplate>
            <div class="col-md-12" style="height: 100px">
                <div class="block">
                    <div class="block-title">
                        <h2>Reviews and ratings</h2>
                        <span style="float: right;">
                            <asp:HiddenField ID="HfId" runat="server" />
                            <asp:Label ID="lblmsg" runat="server" CssClass="label-display"></asp:Label>
                        </span>
                    </div>
                    <div class="form-horizontal form-bordered">
                        <div class="form-group" style="border-bottom: none;">
                            <div style="clear: both; height: 15px;">
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-8">
                                    <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                                        CssClass="table table-vcenter table-condensed table-bordered dataTable grid-data"
                                        PageSize="10"
                                        CellPadding="9" OnPageIndexChanging="gvDetails_PageIndexChanging" >
                                        <PagerStyle CssClass="pagination pagination-sm remove-margin" />
                                        <Columns>

                                            <asp:TemplateField HeaderText="Sr. No." ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Patient" ItemStyle-CssClass="gridText-left">
                                                <ItemTemplate>
                                                    <asp:Label CssClass="me" ID="lblPatient" runat="server" Text='<%#Bind("Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Comment" ItemStyle-CssClass="gridText-left">
                                                <ItemTemplate>
                                                    <asp:Label CssClass="me" ID="lblComment" runat="server" Text='<%#Bind("Comment")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                             <asp:TemplateField HeaderText="Rating" ItemStyle-CssClass="gridText-left">
                                                <ItemTemplate>
                                                    <asp:Rating ID="Rating1" runat="server" StarCssClass="Star" WaitingStarCssClass="WaitingStar" EmptyStarCssClass="Star" ReadOnly="true" CurrentRating='<%#Bind("rating") %>'
                                        FilledStarCssClass="FilledStar">
                                    </asp:Rating>
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

