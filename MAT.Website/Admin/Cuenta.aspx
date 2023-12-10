<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Cuenta.aspx.cs" Inherits="Cuenta" Title="Cuenta List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Cuenta List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="CuentaDataSource"
				DataKeyNames="CuentaId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Cuenta.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="CuentaId" HeaderText="Cuenta Id" SortExpression="[CuentaID]" ReadOnly="True" />
				<data:HyperLinkField HeaderText="Cliente Id" DataNavigateUrlFormatString="ClienteEdit.aspx?ClienteId={0}" DataNavigateUrlFields="ClienteId" DataContainer="ClienteIdSource" DataTextField="RazonSocial" />
				<data:BoundRadioButtonField DataField="Estado" HeaderText="Estado" SortExpression="[Estado]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No Cuenta Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnCuenta" OnClientClick="javascript:location.href='CuentaEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:CuentaDataSource ID="CuentaDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:CuentaProperty Name="Cliente"/> 
					<%--<data:CuentaProperty Name="MovimientoCuentaCollection" />--%>
				</Types>
			</DeepLoadProperties>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:CuentaDataSource>
	    		
</asp:Content>



