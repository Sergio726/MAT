<%@ Control Language="C#" ClassName="ButacaFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataButacaId" runat="server" Text="Butaca Id:" AssociatedControlID="dataButacaId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataButacaId" Value='<%# Bind("ButacaId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataNroButaca" runat="server" Text="Nro Butaca:" AssociatedControlID="dataNroButaca" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataNroButaca" Text='<%# Bind("NroButaca") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataNroButaca" runat="server" Display="Dynamic" ControlToValidate="dataNroButaca" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPiso" runat="server" Text="Piso:" AssociatedControlID="dataPiso" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataPiso" Text='<%# Bind("Piso") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataPiso" runat="server" Display="Dynamic" ControlToValidate="dataPiso" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataUbicacion" runat="server" Text="Ubicacion:" AssociatedControlID="dataUbicacion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataUbicacion" Text='<%# Bind("Ubicacion") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataUbicacion" runat="server" Display="Dynamic" ControlToValidate="dataUbicacion" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataTipo" runat="server" Text="Tipo:" AssociatedControlID="dataTipo" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataTipo" Text='<%# Bind("Tipo") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataTipo" runat="server" Display="Dynamic" ControlToValidate="dataTipo" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataTransporteId" runat="server" Text="Transporte Id:" AssociatedControlID="dataTransporteId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataTransporteId" DataSourceID="TransporteIdTransporteDataSource" DataTextField="NroCoche" DataValueField="TransporteId" SelectedValue='<%# Bind("TransporteId") %>' AppendNullItem="true" Required="false" NullItemText="< Please Choose ...>" />
					<data:TransporteDataSource ID="TransporteIdTransporteDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataFila" runat="server" Text="Fila:" AssociatedControlID="dataFila" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataFila" Text='<%# Bind("Fila") %>' MaxLength="2"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPosicion" runat="server" Text="Posicion:" AssociatedControlID="dataPosicion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataPosicion" Text='<%# Bind("Posicion") %>' MaxLength="1"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataCodigoButaca" runat="server" Text="Codigo Butaca:" AssociatedControlID="dataCodigoButaca" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataCodigoButaca" Text='<%# Bind("CodigoButaca") %>' MaxLength="4"></asp:TextBox>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


