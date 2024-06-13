export interface IResponseFoods {
      id: number,
      code: string,
      name: string,
      scientificName: string,
      group: string,
      brand: string,
}[]

 export interface IResponseDetails {
      id: number,
      component: string,
      units: string,
      valuePer100G: string,
      dataType: string,
      minimumValue: string,
      maximumValue: string,
      numberOfDataUsed: string,
      standardDeviation: string,
      references: string,
}
