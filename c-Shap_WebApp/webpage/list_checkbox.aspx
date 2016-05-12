<%@ Page Language="C#" AutoEventWireup="true" CodeFile="list_checkbox.aspx.cs" Inherits="webpage_list_checkbox" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="../css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../css/webpage/list_checkbox.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    

            <div class="bg-gray section">
		<div class="container _width">

			<div class="shadow-box">
				<div class="container">
					<div class="form-container form-line">
						<div class="heading-wrap">
							<div class="heading-line blue">
								<h2 class="heading">title</h2>
							</div>
						</div> 

						<div class="form-group">
							<div class="form-control" style="height: auto">
								<p><b>radio button </b></p>
								<div class="type-container dp-table layout-fix">
									<div class="item">
										<input id="a1" type="radio" value="HONDA" name="rdBrandcar">
										<label for="a1"> 
											<div class="img-wrap"><img class="img" src="../image/motor/honda.png"></div>
											<div class="dot"></div>
										</label>
									</div>
									<div class="item">
										<input id="a2" type="radio" value="SUZUKI" name="rdBrandcar">
										<label for="a2">
											<div class="img-wrap"><img class="img" src="../image/motor/suzuki.png"></div>
											<div class="dot"></div>
										</label>
									</div>
									<div class="item">
										<input id="a3" type="radio" value="YAMAHA" name="rdBrandcar">
										<label for="a3">
											<div class="img-wrap"><img class="img" src="../image/motor/yamaha.png"></div>
											<div class="dot"></div>
										</label>
									</div>
									<div class="item">
										<input id="a4" type="radio" value="BMW" name="rdBrandcar">
										<label for="a4">
											<div class="img-wrap"><img class="img" src="../image/motor/bmw.png"></div>
											<div class="dot"></div>
										</label>
									</div>
									
								</div>
                                <div class="_alert-meg">ยี่ห้อรถ</div>

							</div>
						</div>

						<div class="form-group">
							<div class="dropdwn-wrap">
								<select id="ddlModelcar" name="modelcar" class="form-control">
									<option selected="" disabled="">รุ่นรถ</option>
								</select>
								<div class="_alert-meg">รุ่นรถ</div>
							</div>
						</div>

                       <div class="form-group">
							<div class="form-control" style="height: auto">
								<p><b>check box</b></p>
								<div class="type-container dp-table layout-fix">
									<div class="item">
										<input id="Radio1" type="checkbox" value="HONDA" name="rdBrandcar">
										<label for="Radio1"> 
											<div class="img-wrap"><img class="img" src="../image/motor/honda.png"></div>
											<div class="dot"></div>
										</label>
									</div>
									<div class="item">
										<input id="Radio2" type="checkbox" value="SUZUKI" name="rdBrandcar">
										<label for="Radio2">
											<div class="img-wrap"><img class="img" src="../image/motor/suzuki.png"></div>
											<div class="dot"></div>
										</label>
									</div>
									<div class="item">
										<input id="Radio3" type="checkbox" value="YAMAHA" name="rdBrandcar">
										<label for="Radio3">
											<div class="img-wrap"><img class="img" src="../image/motor/yamaha.png"></div>
											<div class="dot"></div>
										</label>
									</div>
									<div class="item">
										<input id="Radio4" type="checkbox" value="BMW" name="rdBrandcar">
										<label for="Radio4">
											<div class="img-wrap"><img class="img" src="../image/motor/bmw.png"></div>
											<div class="dot"></div>
										</label>
									</div>
									
								</div>
                                <div class="_alert-meg">ยี่ห้อรถ</div>

							</div>
						</div>

						<div class="section">
							<div class="dp-table check-container text-left">
								<label for="c1" class="check-wrap">
									<div class="dp-table-cell box-wrap">
										<input id="c1" type="checkbox" name="chkplb">
                                        <div for="c1" class="check-box"></div>
									</div>
									<div class="dp-table-cell text-left">ซื้อ พ.ร.บ. ราคา <strong class="red big">645</strong> บาท</div>
								</label>
							</div>
						</div> 
					</div>
				</div>
			</div>
		</div>
	</div>



    </div>
    </form>
</body>
</html>
