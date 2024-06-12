import { useEffect, useState } from 'react';
import { getDetailsFood } from '../../api/http-foods/request';
import { useParams } from 'react-router-dom';
import { ChakraProvider, Box, Table, Thead, Tbody, Tr, Th, Td, Container, Flex, Spinner } from '@chakra-ui/react';
import { IResponseDetails } from '../../api/http-foods/interface';

const Details = () => {
  const [details, setDetails] = useState<IResponseDetails[]>([]);
  const [loading, setLoading] = useState(true);
  const { id } = useParams();

  useEffect(() => {
    const getDetails = async (code: string) => {
      console.log('Fetching details for ID:', code);
      const data = await getDetailsFood(code);
      setDetails(data);
      setLoading(false);
      console.log('Details fetched:', data);
    } 
    if (id) {
      getDetails(id);
    }
  }, [id]);

  if (loading) {
    return (
      <Flex marginTop="20%" alignItems="center" justifyContent="center">
        <Spinner size="xl" />
     </Flex>
    )
  }

  return (
      <ChakraProvider>
        <Container maxW="container.xl" p={4}>
          <Box overflowX="auto">
            <Table variant="striped" colorScheme="teal">
              <Thead>
                <Tr>
                  <Th>Componente</Th>
                  <Th>Unidades</Th>
                  <Th>Valor por 100 g</Th>
                  <Th>Desvio Padrão</Th>
                  <Th>Valor Mínimo</Th>
                  <Th>Valor Máximo</Th>
                  <Th>Número de dados utilizados</Th>
                  <Th>Referências</Th>
                  <Th>Tipo de dados</Th>
                </Tr>
              </Thead>
              <Tbody>
                {details.map((element, indx) => {
                  return (
                    <Tr key={indx}>
                      <Td>{element.component}</Td>
                      <Td>{element.units}</Td>
                      <Td>{element.valuePer100G}</Td>
                      <Td>{element.standardDeviation}</Td>
                      <Td>{element.minimumValue}</Td>
                      <Td>{element.maximumValue}</Td>
                      <Td>{element.numberOfDataUsed}</Td>
                      <Td>{element.references}</Td>
                      <Td>{element.dataType}</Td>
                    </Tr>
                  )
                })}
              </Tbody>
            </Table>
          </Box>
        </Container>
      </ChakraProvider>
    );
}

export default Details;