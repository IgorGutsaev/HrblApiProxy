How to generate openapi/swagger notation from wadl or openapi3:
1. go to https://www.apimatic.io/
2. Import your format
3. Export as you wish
4. Change in file GetDistributorVolumePoints response from

     "responses": {
          "200": {
            "description": "",
            "headers": {}
          }
        },
		
	to
		
	 "responses": {
          "200": {
            "description": "",
            "schema": {
              "xml": {
                "name": "GetDistributorVolumePointsResponse",
                "attribute": false,
                "wrapped": false
              },
              "type": "string"
            },
            "headers": {}
          },
          "400": {
            "description": "",
            "schema": {}
          }
        },
		

Swaggerhub urls
    https://app.swaggerhub.com/apis/rameshn/HLORD/0.1#/
    https://app.swaggerhub.com/apis-docs/rameshn/HLORD/0.1


5. Find api specification for NSwag Filuet.Hrbl.Ordering.SDK\SDK\Api-specification-for-NSwag.txt
6. Generate client with Hrbl.RestApi.nswag
7. run powershell script Filuet.Hrbl.Ordering.SDK\SDK\runafter.ps1