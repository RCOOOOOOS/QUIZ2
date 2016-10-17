<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Products</title>
    <link href="../../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../../Content/font-awesome.min.css" rel="stylesheet" />
</head>
<body>
    <div class="container">
        <h1 class="text-center">View Products</h1>
        <div class="col-lg-30">
            <div class="well">
                <form runat="server" class="form-horizontal">
                    <asp:Panel ID="pnlProducts" runat="server"
                        DefaultButton="btnSearch">
                        <div class="col-lg-6 pull-right">
                        <div class="input-group">
                            <asp:TextBox ID="txtKeyword" runat="server"
                                class="form-control" placeholder="Keyword..." />
                            <span class="input-group-btn">
                                <asp:LinkButton ID="btnSearch" runat="server"
                                    class="btn btn-info" OnClick="btnSearch_Click">
                                    <i class="fa fa-search"></i>
                                </asp:LinkButton>
                            </span>
                        </div>
                    </div>
                    <div class="row"></div>
                    <br />
                    <table class="table table-hover">
                        <thead>
                            <th>#</th>
                            <th>Product Name</th>
                            <th>Category</th>
                            <th>Description</th>
                            <th>Image</th>
                            <th>Price</th>
                            <th>Is Featured?</th>
                            <th>Date Added</th>
                            <th>Date Modified</th>
                            <th></th>
                        </thead>
                        <tbody>
                            <asp:ListView ID="lvProducts" runat="server"
                                OnPagePropertiesChanging="lvProducts_PagePropertiesChanging" >
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("ProductID") %></td>
                                        <td><%# Eval("Name") %></td>
                                        <td><%# Eval("Category") %></td>
                                        <td><%# Eval ("Description") %></td>
                                        <td><img src='/img/<%# Eval("Image") %>' style  ="width:300px; height:200px;" /></td>
                                        <td><%# Eval ("Price") %></td>
                                        <td><%# Eval ("IsFeatured") %></td>
                                        <td><%# Eval ("DateAdded", "{0: MMMM dd, yyyy hh:mm tt}") %></td>
                                        <td><%# Eval ("DateModified", "{0: MMMM dd, yyyy hh:mm tt}") %></td>
                                        <td>
                                            <a href='Details.aspx?ID=<%# Eval("ProductID") %>'
                                                class="btn btn-xs btn-info">
                                                <i class="fa fa-edit"></i>
                                            </a>
                                            <a href='Delete.aspx?ID=<%# Eval("ProductID") %>'
                                                class="btn btn-xs btn-danger"
                                                onclick='return confirm("Are you sure you want to delete this?");'>
                                                <i class="fa fa-trash"></i>
                                            </a>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <EmptyDataTemplate>
                                    <tr>
                                        <td colspan="10">
                                            <h2 class="text-center">
                                                No records found!
                                            </h2>
                                        </td>
                                    </tr>
                                </EmptyDataTemplate>
                            </asp:ListView>
                        </tbody>
                    </table>
                        <hr/>
                        <asp:DataPager ID="dpProducts" runat="server" PageSize="5"
                            PagedControlID="lvProducts">
                            <Fields>
                                <asp:NumericPagerField ButtonType="Button"
                                    CurrentPageLabelCssClass="btn bnt-info"
                                    NumericButtonCssClass="btn"
                                    NextPreviousButtonCssClass="btn" />
                            </Fields>
                        </asp:DataPager>
                    </asp:Panel>
                </form>
            </div>
        </div>
    </div>
</body>
</html>
