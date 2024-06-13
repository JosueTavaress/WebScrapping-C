import { useState, useEffect } from 'react';
import { getFoods, FilterFoods } from '../../api/http-foods/request';
import { 
  Button,
  Input,
  Table,
  Thead,
  Tbody,
  Tr,
  Th,
  Td,
  Spinner,
  Box,
  Flex,
  IconButton,
  useToast,
  HStack,
  Spacer
} from '@chakra-ui/react';
import { SearchIcon, ArrowLeftIcon, ArrowRightIcon, CloseIcon } from '@chakra-ui/icons';
import { useNavigate } from "react-router-dom";
import { IResponseFoods } from '../../api/http-foods/interface';

const Home = () => {
  const take = 100;
  const [page, setPage] = useState(1);
  const [dataItems, setDataItems] = useState<IResponseFoods[]>([]);
  const [searchQuery, setSearchQuery] = useState('');
  const [loading, setLoading] = useState(true);
  const [isSearching, setIsSearching] = useState(false);
  const toast = useToast();
  const navigator = useNavigate();

  const handlerNextPage = () => {
    setPage((prev) => prev + 100);
  }

  const handlerPreviousPage = () => {
    if (page > 25) {
      setPage((prev) => prev - 100);
    } else {
      toast({
        title: "You are already on the first page.",
        status: "info",
        duration: 2000,
        isClosable: true,
      });
    }
  }

  const handleSearch = async () => {
    if (!searchQuery.trim()) return;
    setLoading(true);
    setIsSearching(true);
    const data = await FilterFoods(searchQuery, "1");
    setDataItems(data);
    setPage(1);
    setLoading(false);
  }

  const handleClearSearch = async () => {
    setLoading(true);
    setIsSearching(false);
    setSearchQuery('');
    const data = await getFoods(1, take);
    setDataItems(data);
    setPage(1);
    setLoading(false);
  }

  const handleRowClick = (code: string) => {
    navigator(`/Details/${code}`);
  }

  useEffect(() => {
    const fetchData = async (pageNumber = 1) => {
      setLoading(true);
      let data;
      if (isSearching) {
        data = await FilterFoods(searchQuery, pageNumber.toString());
      } else {
        data = await getFoods(pageNumber, take);
      }
      setDataItems(data);
      setLoading(false);
    }
    fetchData(page);
  }, [page, isSearching]);

  return (
    <Box p={5} display="flex" justifyContent="center">
      <Box width={{ base: "100%", md: "80%", lg: "60%" }}>
        <Flex mb={4} alignItems="center" justify="center">
          <HStack spacing={2} width="100%">
            <Input
              placeholder="Search for food..."
              value={searchQuery}
              onChange={(e) => setSearchQuery(e.target.value)}
              size="sm"
            />
            <IconButton
              icon={<SearchIcon />} 
              onClick={handleSearch} 
              aria-label="Search"
              size="sm"
              colorScheme="blue"
            />
            <IconButton 
              icon={<CloseIcon />} 
              onClick={handleClearSearch} 
              aria-label="Clear Search"
              size="sm"
              colorScheme="red"
            />
          </HStack>
        </Flex>
        {loading ? (
          <Flex marginTop="25%" justifyContent="center">
            <Spinner size="xl" />
          </Flex>
        ) : (
          <Box overflowX="auto" width="100%">
            <Table variant="striped" colorScheme="teal" size="sm" width="100%">
              <Thead>
                <Tr bg="blue.500">
                  <Th color="white">Código</Th>
                  <Th color="white">Nome</Th>
                  <Th color="white">Nome Científico</Th>
                  <Th color="white">Grupo</Th>
                  <Th color="white">Marca</Th>
                </Tr>
              </Thead>
              <Tbody>
                {dataItems.map((item, index) => (
                  <Tr
                    height="53" 
                    key={index} 
                    onClick={() => handleRowClick(item.code)}
                    cursor="pointer"
                    _hover={{ backgroundColor: "blue.100" }} // Add hover effect
                  >
                    <Td>{item.code}</Td>
                    <Td>{item.name}</Td>
                    <Td>{item.scientificName}</Td>
                    <Td>{item.group}</Td>
                    <Td>{item.brand}</Td>
                  </Tr>
                ))}
              </Tbody>
            </Table>
            <Flex mt={4} justifyContent="space-between">
              <Button 
                leftIcon={<ArrowLeftIcon />} 
                onClick={handlerPreviousPage} 
                isDisabled={page === 1}
                colorScheme="blue"
              >
                Anterior
              </Button>
              <Spacer />
              <Button 
                rightIcon={<ArrowRightIcon />} 
                onClick={handlerNextPage}
                colorScheme="blue"
                isDisabled={dataItems.length === 0}
              >
                Próximo
              </Button>
            </Flex>
          </Box>
        )}
      </Box>
    </Box>
  )
}

export default Home;
