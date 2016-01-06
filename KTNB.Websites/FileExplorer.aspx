<%@ Page Title="" Language="C#" MasterPageFile="~/Share/VPB.master" AutoEventWireup="true" CodeBehind="FileExplorer.aspx.cs" Inherits="VPB_KTNB.FileExplorer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div ng-controller="fileExplorer" ng-init="initFunc">
        <div class="row">
            <div class="col-sm-12">
                <h4>File Explorer</h4>
            </div>
        </div>
        <br />
        <div class="file-options">
            <div class="row">
                <div class="col-sm-3">
                    <label for="directory-name-file-explorer">Thư mục:</label>
                    <select id="directory-name-file-explorer" class="form-control">
                        <option>Thư mục - Root</option>
                    </select>
                </div>
                <div class="col-sm-offset-1 col-sm-3">
                    <label for="keywords-file-explorer">Từ khóa tìm kiếm</label>
                    <input id="keywords-file-explorer" type="text" class="form-control" placeholder="Từ khóa tìm kiếm..." />
                </div>

                <div class="col-sm-1">
                    <label>&nbsp;</label>
                    <button type="button" class="btn btn-primary">Tìm</button>
                </div>
            </div>
        </div>

        <div class="file-results">
        </div>
    </div>
</asp:Content>
