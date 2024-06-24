<%@ Page Title="" Language="C#" MasterPageFile="~/Master/MasterPage.master" AutoEventWireup="true" CodeFile="DashBoard.aspx.cs" Inherits="Patient_DashBoard" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <script src="../js/1.7.2_jquery.min.js" type="text/javascript"></script>
    <style type="text/css">
        .form-horizontal .control-label
        {
            padding-top: 0px;
        }
    </style>
    <script type="text/javascript" language="javascript">
       

        function getCommand(element, text) {

            if (text == 'View') {

                window.location = "Review.aspx?TreatmentId=" + element;
            }
            //else {

            //    window.location = "EnquiryList.aspx?EnquiryId=" + element + "&Status=" + text;
            //}

        }

    </script>
    <div id="scrollbar1">
        <div class="col-md-12">
            <div class="form-horizontal form-bordered">
                <div class="form-group" style="border-bottom: none;">
                    <div class="col-md-12"  id="DivEnquiry">
                       
                        <asp:Repeater ID="RptEnquiryList" runat="server" >
                            <ItemTemplate>
                                <div class="col-sm-6 col-lg-4">
                                    <div class="widget">
                                     <%--   <div class="widget-simple">                                           
                                            <h3 class="widget-content animation-pullDown">
                                                <asp:HiddenField ID="hfid1" runat="server" Value='<%#Bind("EventId")%>' />
                                                <asp:Label CssClass="me" ID="lbl1" runat="server" Text='<%#Bind("EventName")%>'></asp:Label>
                                                <span style="float: right; margin-top: -8px;"><strong>
                                                    <asp:LinkButton CssClass="btn btn-sm btn-primary" ID="LinkButton1" runat="server"
                                                       CausesValidation="false"   Text="View" OnClientClick='<%# @"getCommand("""+Eval("EventId").ToString()+@""","""+"View"+@""");return false;" %>'></asp:LinkButton>&nbsp;&nbsp;
                                                </strong></span>
                                            </h3>
                                            <span style="float: left; margin-top: 25px;">Event Date:<strong>  
                                             <asp:Label CssClass="me" ID="link1" runat="server" Text='<%# Convert.ToDateTime(Eval("EventDate")).ToString("dd/MMM/2015")%>'></asp:Label>&nbsp;&nbsp;                                           
                                           
                                              </strong></span><span style="float: left; margin-top: 25px;">Start Date:<strong>
                                                <asp:Label CssClass="me" ID="link2" runat="server" ForeColor="Green" Text='<%# Convert.ToDateTime(Eval("StartDate")).ToString("dd/MMM/2015")%>'></asp:Label>&nbsp;&nbsp;
                                            </strong></span><span style="float: left; margin-top: 25px;">Last Date:<strong>
                                                <asp:Label CssClass="me" ID="link3" runat="server" ForeColor="Red" Text='<%# Convert.ToDateTime(Eval("LastDate")).ToString("dd/MMM/2015")%>'></asp:Label>&nbsp;&nbsp;
                                            </strong></span>
                                            <span style="float: left; margin-top: 25px;">College:<strong>
                                                <asp:Label CssClass="me" ID="lblCollege" runat="server" ForeColor="OrangeRed" Text='<%# Eval("CollegeName")%>'></asp:Label>&nbsp;&nbsp;
                                            </strong></span>
                                        </div>--%>
                                    </div>
                                </div>
                                </table>
                            </ItemTemplate>
                        </asp:Repeater>                        
                        <div style="clear: both;">
                        </div>
                    </div>
                </div>
            </div>
            <div style="clear: both; height: 5px;">
            </div>
           
        </div>
    </div>
</asp:Content>

