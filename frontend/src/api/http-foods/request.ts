import { IResponseDetails, IResponseFoods } from "./interface";
import request  from '../http-config';


const getFoods = async (skip: number, take: number): Promise<IResponseFoods[]> => {
  const response = await request({
    method: "GET",
    url: `Foods/skip/${skip}/take/${take}`,
  });
  return response.data;
}

const FilterFoods = async (input: string, skip: string, take: string = "25") => {
  const params = new URLSearchParams();
  params.append('name', input);
  params.append('skip', skip);
  params.append('take', take);

  const response = await request({
    method: "GET",
    url: `Foods/filter`,
    params
  });
  return response.data;
}

const getDetailsFood = async (code: string): Promise<IResponseDetails[]> => {
  const response = await request({
    method: "GET",
    url: `Foods/details/${code}`,
  });
  return response.data;
}

export {
  getFoods,
  FilterFoods,
  getDetailsFood
}