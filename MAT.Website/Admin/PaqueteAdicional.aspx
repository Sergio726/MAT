<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="PaqueteAdicional.aspx.cs" Inherits="PaqueteAdicional" Title="PaqueteAdicional List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Paquete Adicional List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="PaqueteAdicionalDataSource"
				DataKeyNames="PaqueteAdicionalId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_PaqueteAdicional.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="PaqueteAdicionalId" HeaderText="Paquete Adicional Id" SortExpression="[PaqueteAdicionalID]" ReadOnly="True" />
				<data:HyperLinkField HeaderText="Paquete Id" DataNavigateUrlFormatString="PaqueteEdit.aspx?PaqueteId={0}" DataNavigateUrlFields="PaqueteId" DataContainer="PaqueteIdSource" DataTextField="Descripcion" />
				<data:HyperLinkField HeaderText="Adicional Id" DataNavigateUrlFormatString="AdicionalEdit.aspx?AdicionalId={0}" DataNavigateUrlFields="AdicionalId" DataContainer="AdicionalIdSource" DataTextField="Monto" />
			</Columns>
			<EmptyDataTemplate>
				<b>No PaqueteAdicional Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnPaqueteAdicional" OnClientClick="javascript:location.href='PaqueteAdicionalEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:PaqueteAdicionalDataSource ID="PaqueteAdicionalDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:PaqueteAdicionalProperty Name="Adicional"/> 
					<data:PaqueteAdicionalProperty Name="Paquete"/> 
				</Types>
			</DeepLoadProperties>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:PaqueteAdicionalDataSource>
	    		
</asp:Content>



