using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests
{
    public sealed class ForTests
    {
        public List<string> GetRankedCourses(string user)
        {
            if (string.IsNullOrEmpty(user))
            {
                throw new ArgumentException();
            }
            var myCourses = new HashSet<string>(getAttendedCoursesForUser(user));

            var allCourses = new Dictionary<string, int>();
            var directFriends = getDirectFriendsForUser(user);
            foreach (var friend in directFriends)
            {
                var courses = getAttendedCoursesForUser(friend);
                foreach (var course in courses)
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
            if (!IsInRange(cityLength) || !IsInRange(cityWidth) || !IsInRange(lockerXCoordinates) ||
                !IsInRange(lockerYCoordinates))
            {
                throw new ArgumentException();
            }
            var result = new int[cityLength][];
            for (var i = 0; i < cityLength; i++)
            {
                var blocks = new int[cityWidth];
                for (var j = 0; j < cityWidth; j++)
                {
                    blocks[j] = NumberOfBlockToTheClosestLocker(i, j, lockerXCoordinates, lockerYCoordinates);
                }
                result[i] = blocks;
            }
            return result;
        }

        private static bool IsInRange(int value)
        {
            const int Min = 1;
            const int Max = 9;
            return value <= Max && value >= Min;
        }

        private static bool IsInRange(int[] array)
        {
            return array.All(IsInRange);
        }

        private static int NumberOfBlockToTheClosestLocker(int cityX, int cityY, int[] lockerXCoordinates,
            int[] lockerYCoordinates)
        {
            var result = int.MinValue;
            for (var i = 0; i < lockerXCoordinates.Length; i++)
            {
                var dummy = Math.Abs(lockerXCoordinates[i] - cityX - 1) + Math.Abs(lockerYCoordinates[i] - cityY - 1);
                if (dummy > result)
                {
                    result = dummy;
                }
            }
            return result;
        }

        [Fact]
        public void Test()
        {
        }
    }
}