<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="PasajeroMenor.aspx.cs" Inherits="PasajeroMenor" Title="PasajeroMenor List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Pasajero Menor List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="PasajeroMenorDataSource"
				DataKeyNames="Id"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_PasajeroMenor.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="Pasajeid" HeaderText="Pasajeid" SortExpression="[pasajeid]"  />
				<data:HyperLinkField HeaderText="Pasajeroid" DataNavigateUrlFormatString="ClienteEdit.aspx?ClienteId={0}" DataNavigateUrlFields="ClienteId" DataContainer="PasajeroidSource" DataTextField="RazonSocial" />
				<data:HyperLinkField HeaderText="Menorid" DataNavigateUrlFormatString="ClienteEdit.aspx?ClienteId={0}" DataNavigateUrlFields="ClienteId" DataContainer="MenoridSource" DataTextField="RazonSocial" />
			</Columns>
			<EmptyDataTemplate>
				<b>No PasajeroMenor Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnPasajeroMenor" OnClientClick="javascript:location.href='PasajeroMenorEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:PasajeroMenorDataSource ID="PasajeroMenorDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
			EnableDeepLoad="True"
			>
			<DeepLoadProperties Method="IncludeChildren" Recursive="False">
	            <Types>
					<data:PasajeroMenorProperty Name="Cliente"/> 
				</Types>
			</DeepLoadProperties>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:PasajeroMenorDataSource>
	    		
</asp:Content>



