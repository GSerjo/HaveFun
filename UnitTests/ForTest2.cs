using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public class ForTest2
    {
        private static int[][] GetLockerDistanceGrid(int cityLength, int cityWidth, int[] lockerXCoordinates,
            int[] lockerYCoordinates)
        {
            if (lockerXCoordinates == null || lockerYCoordinates == null)
            {
                throw new NullReferenceException();
            }
            if (lockerXCoordinates.Length != lockerYCoordinates.Length)
            {
                throw new ArgumentException();
            }
            if (!InRange(cityLength) || !InRange(cityWidth) || !InRange(lockerXCoordinates) ||
                !InRange(lockerYCoordinates))
            {
                throw new ArgumentException();
            }
            var result = new int[cityWidth][];
            for (var i = 1; i <= cityWidth; i++)
            {
                var blocks = new int[cityLength];
                for (var j = 1; j <= cityLength; j++)
                {
                    blocks[j - 1] = NumberOfBlockToTheClosestLocker(j, i, lockerXCoordinates, lockerYCoordinates);
                }
                result[i - 1] = blocks;
            }
            return result;
        }

        private static bool InRange(int value)
        {
            // should be private consts, not local(didn't see how to set up private conts in this environment)
            const int Min = 1;
            const int Max = 9;
            return value >= Min && value <= Max;
        }

        private static bool InRange(int[] array)
        {
            return array.All(InRange);
        }

        private static int NumberOfBlockToTheClosestLocker(int cityX, int cityY, int[] lockerXCoordinates,
            int[] lockerYCoordinates)
        {
            var result = int.MaxValue;
            for (var i = 0; i < lockerXCoordinates.Length; i++)
            {
                var dummy = Math.Abs(lockerXCoordinates[i] - cityX) + Math.Abs(lockerYCoordinates[i] - cityY);
                if (dummy < result)
                {
                    result = dummy;
                }
            }
            return result;
        }


        public List<string> GetRankedCourses(string user)
        {
            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentException();
            }

            var directFriends = getDirectFriendsForUser(user);
            if (directFriends == null || directFriends.Count == 0)
            {
                return new List<string>();
            }

            // suppose this method is implemented correctly and returns an empty List not null if the user doesn't have courses, otherwise checking for null is required
            var attendedCoursesForUser = getAttendedCoursesForUser(user);
            var myCourses = new HashSet<string>(attendedCoursesForUser);

            var allCourses = new Dictionary<string, int>();
            foreach (var friend in directFriends)
            {
                // here's potential N+1 issue
                var coursesForUser = getAttendedCoursesForUser(friend);
                foreach (var course in coursesForUser)
                {
                    if (myCourses.Contains(course))
                    {
                        continue;
                    }
                    if (allCourses.ContainsKey(course))
                    {
                        allCourses[course]++;
                    }
                    else
                    {
                        allCourses[course] = 1;
                    }
                }
            }
            return allCourses.OrderBy(x => x.Value).Select(x => x.Key).ToList();
        }

        public List<string> getDirectFriendsForUser(string user)
        {
            throw new NotImplementedException();
        }

        public List<string> getAttendedCoursesForUser(string user)
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Test()
        {
            var result = GetLockerDistanceGrid(3, 5, new[] {1}, new[] {1});
//            var result = GetLockerDistanceGrid(5, 7, new[] {2,4}, new[] {3,7});
            Console.WriteLine("done");
        }
    }
}