<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Voucher.aspx.cs" Inherits="Voucher" Title="Voucher List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Voucher List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="VoucherDataSource"
				DataKeyNames="VoucherId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Voucher.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<data:HyperLinkField HeaderText="Voucher Id" DataNavigateUrlFormatString="VoucherEdit.aspx?VoucherId={0}" DataNavigateUrlFields="VoucherId" DataContainer="VoucherIdSource" DataTextField="NroVoucher" />
				<asp:BoundField DataField="FechaEmision" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Fecha Emision" SortExpression="[FechaEmision]"  />
				<data:HyperLinkField HeaderText="Vendedor Id" DataNavigateUrlFormatString="VendedorEdit.aspx?VendedorId={0}" DataNavigateUrlFields="VendedorId" DataContainer="VendedorIdSource" DataTextField="Descripcion" />
			</Columns>
			<EmptyDataTemplate>
				<b>No Voucher Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnVoucher" OnClientClick="javascript:location.href='VoucherEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:VoucherDataSource ID="VoucherDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:VoucherProperty Name="Vendedor"/> 
					<data:VoucherProperty Name="Voucher"/> 
					<%--<data:VoucherProperty Name="PasajeCollection" />--%>
				</Types>
			</DeepLoadProperties>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:VoucherDataSource>
	    		
</asp:Content>



