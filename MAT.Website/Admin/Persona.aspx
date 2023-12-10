<%@ Page Language="C#" Theme="Default" MasterPageFile="~/MasterPages/admin.master" AutoEventWireup="true"  CodeFile="Persona.aspx.cs" Inherits="Persona" Title="Persona List" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">Persona List</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<data:GridViewSearchPanel ID="GridViewSearchPanel1" runat="server" GridViewControlID="GridView1" PersistenceMethod="Session" />
		<br />
		<data:EntityGridView ID="GridView1" runat="server"			
				AutoGenerateColumns="False"					
				OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
				DataSourceID="PersonaDataSource"
				DataKeyNames="PersonaId"
				AllowMultiColumnSorting="false"
				DefaultSortColumnName="" 
				DefaultSortDirection="Ascending"	
				ExcelExportFileName="Export_Persona.xls"  		
			>
			<Columns>
				<asp:CommandField ShowSelectButton="True" ShowEditButton="True" />				
				<asp:BoundField DataField="PersonaId" HeaderText="Persona Id" SortExpression="[PersonaID]" ReadOnly="True" />
				<asp:BoundField DataField="Apellido" HeaderText="Apellido" SortExpression="[Apellido]"  />
				<asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="[Nombre]"  />
				<asp:BoundField DataField="TipoDocumento" HeaderText="Tipo Documento" SortExpression="[TipoDocumento]"  />
				<asp:BoundField DataField="NroDocumento" HeaderText="Nro Documento" SortExpression="[NroDocumento]"  />
				<asp:BoundField DataField="Celular" HeaderText="Celular" SortExpression="[Celular]"  />
				<asp:BoundField DataField="Telefono" HeaderText="Telefono" SortExpression="[Telefono]"  />
				<asp:BoundField DataField="Email" HeaderText="Email" SortExpression="[Email]"  />
				<asp:BoundField DataField="FechaNacimiento" DataFormatString="{0:d}" HtmlEncode="False" HeaderText="Fecha Nacimiento" SortExpression="[FechaNacimiento]"  />
				<asp:BoundField DataField="LocalidadId" HeaderText="Localidad Id" SortExpression="[LocalidadID]"  />
				<asp:BoundField DataField="UserId" HeaderText="User Id" SortExpression="[UserId]"  />
				<asp:BoundField DataField="Domicilio" HeaderText="Domicilio" SortExpression="[Domicilio]"  />
				<asp:BoundField DataField="Sexo" HeaderText="Sexo" SortExpression="[Sexo]"  />
				<asp:BoundField DataField="Ocupacion" HeaderText="Ocupacion" SortExpression="[Ocupacion]"  />
				<asp:BoundField DataField="Nacionalidad" HeaderText="Nacionalidad" SortExpression="[Nacionalidad]"  />
				<asp:BoundField DataField="PaisResidencia" HeaderText="Pais Residencia" SortExpression="[PaisResidencia]"  />
				<asp:BoundField DataField="Provincia" HeaderText="Provincia" SortExpression="[Provincia]"  />
			</Columns>
			<EmptyDataTemplate>
				<b>No Persona Found!</b>
			</EmptyDataTemplate>
		</data:EntityGridView>
		<br />
		<asp:Button runat="server" ID="btnPersona" OnClientClick="javascript:location.href='PersonaEdit.aspx'; return false;" Text="Add New"></asp:Button>
		<data:PersonaDataSource ID="PersonaDataSource" runat="server"
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
		</data:PersonaDataSource>
	    		
</asp:Content>



