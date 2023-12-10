<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Transporte.aspx.cs" Inherits="Transporte" Title="Transporte List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Transporte List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="TransporteDataSource"
				DataKeyNames="TransporteId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Transporte.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="TransporteId" HeaderText="Transporte Id" SortExpression="[TransporteID]" ReadOnly="True" />
				<asp:BoundField DataField="NroCoche" HeaderText="Nro Coche" SortExpression="[NroCoche]"  />
				<asp:BoundField DataField="MaxPasajeros" HeaderText="Max Pasajeros" SortExpression="[MaxPasajeros]"  />
				<asp:BoundField DataField="KmRecorridos" HeaderText="Km Recorridos" SortExpression="[KmRecorridos]"  />
				<asp:BoundField DataField="UltimoService" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Ultimo Service" SortExpression="[UltimoService]"  />
				<asp:BoundField DataField="Matricula" HeaderText="Matricula" SortExpression="[Matricula]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No Transporte Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnTransporte" OnClientClick="javascript:location.href='TransporteEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:TransporteDataSource ID="TransporteDataSource" runat="server"
			SelectMethod="GetPaged"
			EnablePaging="True"
			EnableSorting="True"
		>
			<Parameters>
				<data:CustomParameter Name="WhereClause" Value="" ConvertEmptyStringToNull="false" />
				<data:CustomParameter Name="OrderByClause" Value="" ConvertEmptyStringToNull="false" />
				<asp:ControlParameter Name="PageIndex" ControlID="GridView1" PropertyName="PageIndex" Type="Int32" />
				<asp:ControlParameter Name="PageSize" ControlID="GridView1" PropertyName="PageSize" Type="Int32" />
				<data:CustomParameter Name="RecordCount" Value="0" Type="Int32" />
			</Parameters>
		</data:TransporteDataSource>
	    		
</asp:Content>



