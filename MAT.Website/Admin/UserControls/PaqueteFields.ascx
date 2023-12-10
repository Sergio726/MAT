<%@ Control Language="C#" ClassName="PaqueteFields" %>

<asp:FormView ID="FormView1" runat="server">
	<ItemTemplate>
		<table border="0" cellpadding="3" cellspacing="1">
			<tr>
        <td class="literal"><asp:Label ID="lbldataPaqueteId" runat="server" Text="Paquete Id:" AssociatedControlID="dataPaqueteId" /></td>
        <td>
					<asp:HiddenField runat="server" id="dataPaqueteId" Value='<%# Bind("PaqueteId") %>'></asp:HiddenField>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataDescripcion" runat="server" Text="Descripcion:" AssociatedControlID="dataDescripcion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataDescripcion" Text='<%# Bind("Descripcion") %>' MaxLength="100"></asp:TextBox><asp:RequiredFieldValidator ID="ReqVal_dataDescripcion" runat="server" Display="Dynamic" ControlToValidate="dataDescripcion" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPrecioCama" runat="server" Text="Precio Cama:" AssociatedControlID="dataPrecioCama" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataPrecioCama" Text='<%# Bind("PrecioCama") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataPrecioCama" runat="server" Display="Dynamic" ControlToValidate="dataPrecioCama" ErrorMessage="Invalid value" MaximumValue="999999999" MinimumValue="-999999999" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataMoneda" runat="server" Text="Moneda:" AssociatedControlID="dataMoneda" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataMoneda" Text='<%# Bind("Moneda") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataMoneda" runat="server" Display="Dynamic" ControlToValidate="dataMoneda" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataIva" runat="server" Text="Iva:" AssociatedControlID="dataIva" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataIva" Text='<%# Bind("Iva") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataAlicuota" runat="server" Text="Alicuota:" AssociatedControlID="dataAlicuota" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataAlicuota" Text='<%# Bind("Alicuota") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataTemporada" runat="server" Text="Temporada:" AssociatedControlID="dataTemporada" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataTemporada" Text='<%# Bind("Temporada") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataTemporada" runat="server" Display="Dynamic" ControlToValidate="dataTemporada" ErrorMessage="Invalid value" MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataCotizacion" runat="server" Text="Cotizacion:" AssociatedControlID="dataCotizacion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataCotizacion" Text='<%# Bind("Cotizacion") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataCotizacion" runat="server" Display="Dynamic" ControlToValidate="dataCotizacion" ErrorMessage="Invalid value" MaximumValue="999999999" MinimumValue="-999999999" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataCodigo" runat="server" Text="Codigo:" AssociatedControlID="dataCodigo" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataCodigo" Text='<%# Bind("Codigo") %>' MaxLength="50"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataDestinoId" runat="server" Text="Destino Id:" AssociatedControlID="dataDestinoId" /></td>
        <td>
					<data:EntityDropDownList runat="server" ID="dataDestinoId" DataSourceID="DestinoIdLocalidadDataSource" DataTextField="IdDepartamento" DataValueField="Id" SelectedValue='<%# Bind("DestinoId") %>' AppendNullItem="true" Required="true" NullItemText="< Please Choose ...>" ErrorText="Required" />
					<data:LocalidadDataSource ID="DestinoIdLocalidadDataSource" runat="server" SelectMethod="GetAll"  />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPrecioSemiCama" runat="server" Text="Precio Semi Cama:" AssociatedControlID="dataPrecioSemiCama" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataPrecioSemiCama" Text='<%# Bind("PrecioSemiCama") %>'></asp:TextBox><asp:RangeValidator ID="RangeVal_dataPrecioSemiCama" runat="server" Display="Dynamic" ControlToValidate="dataPrecioSemiCama" ErrorMessage="Invalid value" MaximumValue="999999999" MinimumValue="-999999999" Type="Double"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataFoto" runat="server" Text="Foto:" AssociatedControlID="dataFoto" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataFoto" Text='<%# Bind("Foto") %>' MaxLength="200"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataServiciosParticulares" runat="server" Text="Servicios Particulares:" AssociatedControlID="dataServiciosParticulares" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataServiciosParticulares" Text='<%# Bind("ServiciosParticulares") %>'  TextMode="MultiLine"  Width="250px" Rows="5"></asp:TextBox>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataFechaCreacion" runat="server" Text="Fecha Creacion:" AssociatedControlID="dataFechaCreacion" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataFechaCreacion" Text='<%# Bind("FechaCreacion", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataFechaCreacion" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" />
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataPublicWeb" runat="server" Text="Public Web:" AssociatedControlID="dataPublicWeb" /></td>
        <td>
					<asp:RadioButtonList runat="server" ID="dataPublicWeb" SelectedValue='<%# Bind("PublicWeb") %>' RepeatDirection="Horizontal"><asp:ListItem Value="True" Text="Yes" Selected="True"></asp:ListItem><asp:ListItem Value="False" Text="No"></asp:ListItem></asp:RadioButtonList>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataLastUpdate" runat="server" Text="Last Update:" AssociatedControlID="dataLastUpdate" /></td>
        <td>
					<asp:TextBox runat="server" ID="dataLastUpdate" Text='<%# Bind("LastUpdate", "{0:d}") %>' MaxLength="10"></asp:TextBox><asp:ImageButton ID="cal_dataLastUpdate" runat="server" SkinID="CalendarImageButton" OnClientClick="javascript:showCalendarControl(this.previousSibling);return false;" /><asp:RequiredFieldValidator ID="ReqVal_dataLastUpdate" runat="server" Display="Dynamic" ControlToValidate="dataLastUpdate" ErrorMessage="Required"></asp:RequiredFieldValidator>
				</td>
			</tr>
			<tr>
        <td class="literal"><asp:Label ID="lbldataModePublicity" runat="server" Text="Mode Publicity:" AssociatedControlID="dataModePublicity" /></td>
        <td>
					<asp:RadioButtonList runat="server" ID="dataModePublicity" SelectedValue='<%# Bind("ModePublicity") %>' RepeatDirection="Horizontal"><asp:ListItem Value="True" Text="Yes" Selected="True"></asp:ListItem><asp:ListItem Value="False" Text="No"></asp:ListItem></asp:RadioButtonList>
				</td>
			</tr>
			
		</table>

	</ItemTemplate>
</asp:FormView>


